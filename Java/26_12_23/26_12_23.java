import java.io.File;
import java.util.Random;
import java.util.Scanner;
import java.io.FileNotFoundException;

//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.

public class Main {

    static void Task_1() {
        Scanner scanner = new Scanner(System.in);

        // Ввод имени файла
        System.out.print("Введите имя файла: ");
        String fileName = scanner.nextLine();

        // Подсчет суммы чисел из файла
        try {
            int sum = calculateSumFromFile(fileName);
            System.out.println("Общая сумма чисел в файле: " + sum);
        } catch (FileNotFoundException e) {
            System.err.println("Файл не найден: " + e.getMessage());
        }
    }
    private static int calculateSumFromFile(String fileName) throws FileNotFoundException {
        File file = new File(fileName);
        Scanner fileScanner = new Scanner(file);

        int sum = 0;

        // Чтение чисел из файла и подсчет суммы
        while (fileScanner.hasNext()) {
            if (fileScanner.hasNextInt()) {
                sum += fileScanner.nextInt();
            } else {
                // Пропустить нечисловые значения
                fileScanner.next();
            }
        }

        // Закрываем Scanner
        fileScanner.close();

        return sum;
    }

    public static void Task_5() throws FileNotFoundException {
        String path;
        path = "C:\\Users\\OMON\\Desktop\\1.java";
        Scanner sc = new Scanner(new File(path));

        StringBuffer result = new StringBuffer();

        int tabsCount = 0;
        while (sc.hasNextLine()) {
            String line = sc.nextLine();

            if (line.contains("}")) tabsCount--;
            result.append("%s%s\n".formatted("\t".repeat(tabsCount), line));
            if (line.contains("{")) tabsCount++;
        }

        System.out.println(result);
        sc.close();
    }


    public static void main(String[] args) {
        try {
            Task_5();
        } catch (FileNotFoundException e) {
            throw new RuntimeException(e);
        }
    }
}