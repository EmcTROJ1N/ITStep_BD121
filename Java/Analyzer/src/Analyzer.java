import javax.naming.NameNotFoundException;
import javax.script.Invocable;
import java.io.*;
import java.lang.annotation.AnnotationTypeMismatchException;
import java.lang.reflect.*;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardOpenOption;
import java.util.*;
import java.util.function.Supplier;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class Analyzer {

    Object SourceObject;
    Class<?> Clazz;
    public Analyzer(Object object) {
        SourceObject = object;
        Clazz = object.getClass();
    }
    public Map<String, Integer> analyzeMethods() {
        Map<String, Integer> methodInfo = new HashMap<>();

        Method[] methods = Clazz.getMethods();

        for (Method method : methods) {
            String methodName = method.getName();
            int parameterCount = method.getParameterCount();
            methodInfo.put(methodName, parameterCount);
        }

        return methodInfo;
    }

    public void runMethod(String methodName, Object... params) {
        Method method;
        try {
            method = Arrays.stream(Clazz.getMethods()).filter(m -> m.getName().equals(methodName)).toArray(Method[]::new)[0];
            method.invoke(SourceObject, params);
        } catch (IndexOutOfBoundsException ex) {
            throw new RuntimeException("Invalid method name");
        } catch (Exception ex) {
            throw new RuntimeException(ex);
        }
    }


    private static Object InitializeNewInstance(Class<?> clazz) {
        try {
            String name = clazz.getSimpleName();
            if (name.equals("int") || name.equals("Integer")) return Integer.valueOf(0);
            else if (name.equals("double") || name.equals("Double")) return Double.valueOf(0);
            else if (name.equals("float") || name.equals("Float")) return Float.valueOf(0);
            else if (name.equals("long") || name.equals("Long")) return Long.valueOf(0);
            else if (name.equals("char") || name.equals("Character")) return Character.valueOf('0');
            else if (name.equals("short") || name.equals("Short")) return Short.valueOf((short)1);
            else if (name.equals("byte") || name.equals("Byte")) return Byte.valueOf((byte)1);
            else if (name.equals("boolean") || name.equals("Boolean")) return Boolean.valueOf(false);
            else if (name.equals("String")) return new String();
            else {
                Constructor<?> constructor = clazz.getConstructor();
                return constructor.newInstance();
            }

        } catch (InstantiationException | IllegalAccessException | InvocationTargetException |
                 NoSuchMethodException e) {
            throw new RuntimeException(e);
        }
    }
    private static List<String> ParseJsonList(String json) {
        if (!(json.startsWith("[") && json.endsWith("]")))
            throw new RuntimeException("Incorrect string for parsing");
        ArrayList<String> splitted = new ArrayList<>();

        char splitter = ',';
        List<Character> includedElements = Arrays.asList( '{', '[', ']', '}');
        StringBuilder currentElement = new StringBuilder();
        boolean escaped = false;
        boolean describeElement = false;
        char[] jsonArr = json.substring(1, json.length() - 1).toCharArray();
        for (int i = 0; i < jsonArr.length; i++) {
            if (includedElements.contains(jsonArr[i]))
                describeElement = !describeElement;
            if (!describeElement && jsonArr[i] == '\"' && jsonArr[i == 0 ? 0 : i - 1] != '\\')
                escaped = !escaped;
            if (!describeElement && !escaped && splitter == jsonArr[i]) {
                splitted.add(currentElement.toString());
                currentElement.setLength(0);
            } else currentElement.append(jsonArr[i]);
        }
        splitted.add(currentElement.toString());

        return splitted;
    }
    private static Map<String, String> ParseJsonObject(String json) {

        if (!(json.startsWith("{") && json.endsWith("}")))
            throw new RuntimeException("Incorrect string for parsing");

        List<String> splitted = ParseJsonList("[%s]".formatted(json.substring(1, json.length() - 1)));

        Map<String, String> fields = new HashMap<>();

        Pattern pattern = Pattern.compile("\"(?<key>.*)\":(?<value>.*)");

        for (String pair: splitted) {
            Matcher matcher = pattern.matcher(pair);

            if (matcher.matches()) {
                String key = matcher.group("key");
                String value = matcher.group("value");
                fields.put(key, value);
            }
        }

        return fields;
    }

    private static Object ParseObject(Class<?> fieldType, String value) throws InvocationTargetException, IllegalAccessException, NoSuchMethodException, InstantiationException, ClassNotFoundException {

        if (fieldType.getSimpleName().equals("Object")) {
            return null;
        }

        if (fieldType.isPrimitive()) {
            Object converted = switch (fieldType.getName()) {
                case "int" -> Integer.parseInt(value);
                case "double" -> Double.parseDouble(value);
                case "char" -> value;
                case "boolean" -> Boolean.parseBoolean(value);
                case "float" -> Float.parseFloat(value);
                case "long" -> Long.parseLong(value);
                case "short" -> Short.parseShort(value);
                case "byte" -> Byte.parseByte(value);
                default -> throw new RuntimeException("Invalid type");
            };
            return converted;
        } else {
            String simpleName = fieldType.getSimpleName();

            List<String> basicTypes = Arrays.asList("Integer", "Double", "Float",
                    "Boolean", "Byte", "Short", "Character", "Long");

            if (basicTypes.contains(simpleName)) {
                Method method = fieldType.getMethod("valueOf", String.class);
                //field.set(result, method.invoke(null, value));
                return method.invoke(null, value);
            } else if (simpleName.equals("String")) return value;

            else if (simpleName.equals("AtomicInteger")) {
                Constructor<?> constructor = fieldType.getConstructor();
                Object atomicInt = constructor.newInstance();
                atomicInt.getClass().getMethod("set", int.class).invoke(atomicInt, Integer.parseInt(value));
                return atomicInt;
            } else if (simpleName.endsWith("[]")) {
                int length = ParseJsonList(value).size();
                return ParseJson(value, () -> Array.newInstance(fieldType.getComponentType(), length));
            } else
                throw new RuntimeException("Unsupported type");

        }
    }

    public static <T> T ParseJson(String json, Supplier<T> construct) throws IllegalAccessException, ClassNotFoundException, NoSuchMethodException, InvocationTargetException, InstantiationException {
        T result = construct.get();
        Class<?> clazz = result.getClass();

        if (json.startsWith("{") && json.endsWith("}")) {
            Map<String, String> parsedFields = ParseJsonObject(json);
            Field[] fields = clazz.getFields();

            for (Field field: fields) {
                if (!parsedFields.containsKey(field.getName()))
                    continue;
                String name = field.getName();
                String value = parsedFields.get(name);

                if (field.getType().getSimpleName().endsWith("[]")) {
                    int length = ParseJsonList(value).size();
                    field.set(result, ParseJson(value, () -> Array.newInstance(field.getType().getComponentType(), length)));
                } else
                    field.set(result, ParseJson(value, () -> InitializeNewInstance(field.getType())));
            }

        } else if (json.startsWith("[") && json.endsWith("]")) {
            List<String> values = ParseJsonList(json);

            for (int i = 0; i < (long)values.size(); i++) {
                Array.set(result, i, ParseJson(values.get(i), () -> InitializeNewInstance(clazz.getComponentType())));
            }
        }
        else result = (T)ParseObject(clazz, json);

        return result;
    }
    public static <T> T Deserialize(String json, Supplier<T> createInstance) {
        json = json.replaceAll(" ", "");
        json = json.replaceAll("\n", "");

        try {
            return ParseJson(json, createInstance);
        } catch (ClassNotFoundException | NoSuchMethodException | IllegalAccessException | InvocationTargetException |
                 InstantiationException e) {
            throw new RuntimeException(e);
        }
    }

    public static void Serialize(Object obj, String fileName) throws IOException {
        FileWriter writer = new FileWriter(fileName);
        writer.write(Serialize(obj));
        writer.close();
    }
    public static String Serialize(Object obj) {
        if (obj == null)
            return "null";

        List<String> DefaultPackages = Arrays.stream(new String[] {
                "java.lang", "java.util"
        }).toList();

        Class<?> clazz = obj.getClass();


        if (clazz.getSimpleName().endsWith("[]"))
            return SerializeArray((Object[])obj);

        StringBuilder serialized = new StringBuilder();

        try {
            if (DefaultPackages.stream().anyMatch(clazz.getPackageName()::startsWith))
                return (String)clazz.getMethod("toString").invoke(obj);

            if (clazz.getAnnotation(Serializable.class) == null)
                throw new RuntimeException("Object not serializable");

            serialized.append("{ ");

            for (Field field : clazz.getFields()) {

                if (field.getAnnotation(SerializeIgnore.class) != null)
                    continue;

                serialized.append("\"%s\": ".formatted(field.getName()));


                Class<?> type = field.getType();
                String typeName = type.getSimpleName();
                String packageName = type.getPackageName();
                if (typeName.endsWith("String"))
                    serialized.append("\"%s\", ".formatted(field.get(obj).toString().replaceAll("\"", "\\\\\"")));
                else if (typeName.endsWith("[]")) {

                    Object srcArr = field.get(obj);
                    Object[] objs = new Object[Array.getLength(srcArr)];
                    for (int i = 0; i < objs.length ; i++)
                        objs[i] = Array.get(srcArr, i);

                    serialized.append("%s, ".formatted(SerializeArray(objs)));

                }
                else if (DefaultPackages.stream().anyMatch(packageName::startsWith))
                    serialized.append("%s, ".formatted(field.get(obj)));
                else
                    serialized.append("%s, ".formatted(Serialize(field.get(obj))));

            }
        } catch (IllegalAccessException | NoSuchMethodException | InvocationTargetException e) {
            throw new RuntimeException(e);
        }

        if (serialized.length() >= 3)
            serialized.deleteCharAt(serialized.length() - 2);
        serialized.append("}");

        return serialized.toString();
    }
    public static String Serialize(int[] arr) {
        Integer[] objArr = new Integer[arr.length];
        for (int i = 0; i < arr.length; i++)
            objArr[i] = arr[i];
        return SerializeArray(objArr);
    }
    public static String Serialize(double[] arr) {
        Double[] objArr = new Double[arr.length];
        for (int i = 0; i < arr.length; i++)
            objArr[i] = arr[i];
        return SerializeArray(objArr);
    }
    public static String Serialize(long[] arr) {
        Long[] objArr = new Long[arr.length];
        for (int i = 0; i < arr.length; i++)
            objArr[i] = arr[i];
        return SerializeArray(objArr);
    }
    public static String Serialize(float[] arr) {
        Float[] objArr = new Float[arr.length];
        for (int i = 0; i < arr.length; i++)
            objArr[i] = arr[i];
        return SerializeArray(objArr);
    }
    public static String Serialize(short[] arr) {
        Short[] objArr = new Short[arr.length];
        for (int i = 0; i < arr.length; i++)
            objArr[i] = arr[i];
        return SerializeArray(objArr);
    }
    public static String Serialize(byte[] arr) {
        Byte[] objArr = new Byte[arr.length];
        for (int i = 0; i < arr.length; i++)
            objArr[i] = arr[i];
        return SerializeArray(objArr);
    }
    public static String Serialize(char[] arr) {
        Character[] objArr = new Character[arr.length];
        for (int i = 0; i < arr.length; i++)
            objArr[i] = arr[i];
        return SerializeArray(objArr);
    }
    public static String Serialize(boolean[] arr) {
        Boolean[] objArr = new Boolean[arr.length];
        for (int i = 0; i < arr.length; i++)
            objArr[i] = arr[i];
        return SerializeArray(objArr);
    }
    private static String SerializeArray(Object[] array) {
        StringBuffer serialized = new StringBuffer();

        serialized.append("[ ");

        for (var item: array)
            serialized.append("%s, ".formatted(Serialize(item)));

        if (serialized.length() >= 2)
            serialized.deleteCharAt(serialized.length() - 2);
        serialized.append("]");

        return serialized.toString();
    }
}