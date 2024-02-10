import jdk.jfr.Timespan;

import javax.swing.*;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.sql.Time;
import java.util.*;
import java.util.concurrent.locks.Lock;
import java.util.function.Consumer;
import java.util.function.Function;
import java.util.function.Supplier;

public class Main {
    public static void WriteCollectionIntoFile(ArrayList<?> collection, String fileName) throws IOException {

        FileWriter FileStream = new FileWriter(fileName);

        collection.forEach(item -> {
            try {
                FileStream.write(item.toString());
            } catch (IOException ex) {
                throw new RuntimeException();
            }
        });

        FileStream.close();
    }

    public static void main(String[] args) {
        int rangeMin = 2;
        //int rangeMax = 100;
        int rangeMax = 5000000;
        String fileName = "result.txt";

        PrimeNumbersController controller = new PrimeNumbersController();

        var result = Stopwatch.Execute(() -> {
            try {
                return controller.BeginFindPrimeNumbers(rangeMin, rangeMax, 8);
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        });

        //result.Result.forEach(System.out::println);
        System.out.printf("Full time: %d", result.Duration());


        for (int i = 1; i < 9; i++) {

            /*
            try {
                controller.BeginFindPrimeNumbers(rangeMin, rangeMax, threadsCount);
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }

            final int threadsCount = i;

            var response = Stopwatch.Execute(() -> {
                try {
                    return controller.BeginFindPrimeNumbers(rangeMin, rangeMax, threadsCount);
                } catch (InterruptedException e) {
                    throw new RuntimeException(e);
                }
            });
            response.Result.forEach(System.out::println);

            //if (i > 1) break;

            System.out.printf("Result time in %d threads: %d%n", i, response.Duration());
             */
        }
    }
}