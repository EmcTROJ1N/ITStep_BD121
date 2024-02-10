import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.Objects;
import java.util.function.Consumer;
import java.util.function.Supplier;

class IntComparator implements Comparator<Integer> {

    @Override
    public int compare(Integer o1, Integer o2) {
        if (o1 > o2) return -1;
        if (o1 < o2) return 1;
        else return 0;
    }
}

public class Main {

    public static void main(String[] args) throws InterruptedException {

        Consumer<ThreadedArrayList<Integer>> listFill = arr -> {
            arr.clear();
            int rangeMin = 0;
            //int rangeMax = 100;
            int rangeMax = 500000;

            for (int i = rangeMax; i > rangeMin; i--)
                arr.add(i);
        };

        ThreadedArrayList<Integer> list = new ThreadedArrayList<>();

        System.out.println("Sort begin!");



        /*
        list.setThreadsCount(8);
        listFill.accept(list);
        var test = Stopwatch.Execute(() -> list.sort(new IntComparator()));
        //list.forEach(System.out::println);
        System.out.println(test.Duration());
         */

        int[] threadsCnt = { 1, 2, 4, 8 };
        //int[] threadsCnt = { 1, 2, 3, 4, 5, 6, 7, 8 };
        for (int i : threadsCnt) {
            //System.out.println("Filling...");
            listFill.accept(list);

            list.setThreadsCount(i);
            var response = Stopwatch.Execute(() -> list.sort(new IntComparator()));
            //list.forEach(System.out::println);

            System.out.printf("Duration in %d threads: %d%n", i, response.Duration());
        }
    }
}