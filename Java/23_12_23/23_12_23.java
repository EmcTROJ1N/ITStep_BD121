import java.util.Random;
import java.util.Scanner;

//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.

public class Main {

    public static void Task_1() {
        Scanner scanner = new Scanner(System.in);

        // Создаем массив из 5 элементов
        int[] array = new int[5];

        // Заполняем массив значениями, введенными пользователем
        System.out.println("Введите 5 элементов массива:");
        for (int i = 0; i < 5; i++) {
            System.out.print("Элемент " + (i + 1) + ": ");
            array[i] = scanner.nextInt();
        }

        // Модификация массива
        for (int i = 0; i < 5; i++) {
            // Если элемент четный, заменяем на 0
            if (array[i] % 2 == 0) {
                array[i] = 0;
            } else {
                // Если элемент нечетный, увеличиваем в 2 раза
                array[i] *= 2;
            }
        }

        // Выводим результат
        System.out.println("Измененный массив:");
        for (int i = 0; i < 5; i++) {
            System.out.print(array[i] + " ");
        }
    }

    public static void Task_2() {
        Scanner scanner = new Scanner(System.in);

        // Создаем массив 3x4
        int[][] array = new int[3][4];

        // Заполняем массив значениями, введенными пользователем
        System.out.println("Введите элементы массива (3x4):");
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 4; j++) {
                System.out.print("Элемент [" + (i + 1) + "][" + (j + 1) + "]: ");
                array[i][j] = scanner.nextInt();
            }
        }

        // Модификация массива: замена троек на пятёрки и наоборот
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 4; j++) {
                if (array[i][j] == 3) {
                    array[i][j] = 5;
                } else if (array[i][j] == 5) {
                    array[i][j] = 3;
                }
            }
        }

        // Выводим результат
        System.out.println("Измененный массив:");
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 4; j++) {
                System.out.print(array[i][j] + " ");
            }
            System.out.println();
        }
    }

    public static void Task_3() {
        int[][] biggerArr = {
                { 1, 2, 3, 4, 5 },
                { 6, 7, 8, 9, 10},
                { 11, 12, 13, 14, 15},
                { 16, 17, 18, 19, 20}
        };

        int[][] smallerArr = {
                { 1, 2, 3 },
                { 6, 7, 8 },
        };

        outerLoop:
        for (int i = 0; i < biggerArr.length - smallerArr.length; i++) {
            for (int j = 0; j < biggerArr[0].length - smallerArr[0].length; j++) {

                boolean flag = true;

                innterLoop:
                for (int k = 0; k < smallerArr.length; k++) {
                    for (int n = 0; n < smallerArr[0].length; n++) {

                        if (biggerArr[k + i][n + j] != smallerArr[k][n]) {
                            flag = false;
                            break innterLoop;
                        }
                    }
                }

                if (flag) {
                    System.out.println("yes, includes");
                    break outerLoop;
                }

            }
        }
    }

    public static void Task_4() {

    }

    public static void Task_5() {
        Scanner scanner = new Scanner(System.in);
        Random random = new Random();

        // Ввод размеров массивов
        System.out.print("Введите количество элементов первого массива: ");
        int size1 = scanner.nextInt();

        System.out.print("Введите количество элементов второго массива: ");
        int size2 = scanner.nextInt();

        // Заполнение первого массива
        int[] array1 = new int[size1];
        System.out.println("Первый массив:");
        fillArrayWithRandomNumbers(array1);

        // Заполнение второго массива
        int[] array2 = new int[size2];
        System.out.println("Второй массив:");
        fillArrayWithRandomNumbers(array2);

        // Вывод массивов на экран
        printArray(array1);
        printArray(array2);

        // Нахождение наиболее длинной общей последовательности
        System.out.println(findLongestCommonSequence(array1, array2));

    }

    private static int findLongestCommonSequence(int[] array1, int[] array2) {
        int maxLength = 0;
        int endIndex = 0;

        for (int i = 0; i < array1.length; i++) {
            for (int j = 0; j < array2.length; j++) {
                int length = 0;
                int index1 = i;
                int index2 = j;

                // Находим общую последовательность
                while (index1 < array1.length && index2 < array2.length && array1[index1] == array2[index2]) {
                    length++;
                    index1++;
                    index2++;
                }

                // Обновляем наибольшую длину
                if (length > maxLength) {
                    maxLength = length;
                    endIndex = index1;
                }
            }
        }
        return maxLength;
    }

    private static void printArray(int[] array) {
        for (int num : array) {
            System.out.print(num + " ");
        }
        System.out.println();
    }


    static void fillArrayWithRandomNumbers(int[] array) {
        Random random = new Random();
        for (int i = 0; i < array.length; i++) {
            array[i] = random.nextInt(10); // случайное число от 0 до 9
        }
    }

    public static void main(String[] args) {
        Task_5();
    }
}