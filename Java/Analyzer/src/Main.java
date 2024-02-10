import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.Serial;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Map;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.function.Supplier;
import java.util.stream.Collectors;

@Serializable
class ExampleObject {
    public int field1 = 666;
    public Integer field2;
    public String field3;
    public AtomicInteger field4;

    public double[] DoubleArray;
    @SerializeIgnore
    public ArrayList<Integer> IntArrayList;
    public Object NullField;
    public InnerObject innerObj;
    public EmptyObject emptyObject;

    @SerializeIgnore
    public NotSerializableObject notSerializableObject;
    private int PrivateField;


    public void method1() {
        System.out.println("method1 called");
    }

    public void method2(int arg1) {
        System.out.println("method2 called with arg1: " + arg1);
    }

    public void method3(String arg1, int arg2) {
        System.out.println("method3 called with arg1: " + arg1 + ", arg2: " + arg2);
    }

    public void displayData() {
        System.out.println("Displaying data for ExampleObject:");

        // Выводим значения выбранных полей
        System.out.println("field1: " + field1);
        System.out.println("field2: " + field2);
        System.out.println("field3: " + field3);
        System.out.println("field4: " + field4);
        System.out.print("doubleArray: ");
        Arrays.stream(DoubleArray).forEach(System.out::print);
        System.out.println();
        System.out.println("nullField: " + NullField);
        System.out.println("innerObj: " + innerObj);
        System.out.println("emptyObj: " + emptyObject);
        System.out.println("IgnoreField: " + notSerializableObject);
        System.out.print("IntArrayList: ");
        IntArrayList.forEach(System.out::print);
        System.out.println();
    }

}

@Serializable
class InnerObject {
    public InnerObject() {}
    public int InnerField = 666;
}

@Serializable
class EmptyObject {
    public EmptyObject() {

    }
}

class NotSerializableObject {}

public class Main {
    public static void main(String[] args) throws JsonProcessingException {

        ExampleObject exampleObject = new ExampleObject();
        exampleObject.field1 = 10;
        exampleObject.field2 = 100;
        exampleObject.DoubleArray = new double[] { 1, 2, 3 };
        exampleObject.field3 = "Hello, world \" hello";
        exampleObject.field4 = new AtomicInteger(100);
        exampleObject.innerObj = new InnerObject();
        exampleObject.emptyObject = new EmptyObject();
        exampleObject.IntArrayList = new ArrayList<>(Arrays.asList(1, 2, 3));

        Analyzer analyzer = new Analyzer(exampleObject);
        ObjectMapper mapper = new ObjectMapper();

        String jacksonJson = mapper.writeValueAsString(exampleObject);
        String json = Analyzer.Serialize(exampleObject);


        ExampleObject jacksonDeserializedObject = mapper.readValue(json, ExampleObject.class);
        ExampleObject deserializedObject = Analyzer.Deserialize(json, ExampleObject::new);

        deserializedObject.displayData();

        //deserializedObject.displayData();

        //String json = Analyzer.Serialize(new double[] { 1, 2, 3 });
        /*
        */
        //displayMethods(analyzer);

        /*
        analyzer.runMethod("method2", 3);

        */

        /*
        Map<String, Integer> result = analyzer.analyzeMethods();
        for (Map.Entry<String, Integer> entry : result.entrySet())
            System.out.println("Method: " + entry.getKey() + ", args: " + entry.getValue());
        */

    }
}