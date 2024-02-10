import java.util.ArrayList;



class PrimeNumbersThread implements Runnable {
    public volatile ArrayList<Integer> Result;
    private PrimeNumbersController _controller;
    private int _from;
    private int _to;
    private int _step;
    private int ThreadNumber = 0;

    public PrimeNumbersThread(PrimeNumbersController controller, int from, int to, int step, int threadNumber) {
        _controller = controller;
        _from = from;
        _to = to;
        _step = step;
        ThreadNumber = threadNumber;
    }

    @Override
    public void run() {
        Result = _controller.FindPrimeNumbersInRange(_from, _to, _step, ThreadNumber);
    }
}

public class PrimeNumbersController {
    public ArrayList<Integer> BeginFindPrimeNumbers(int min, int max, int threadsCount) throws InterruptedException {
        ArrayList<Integer> primeNumbers = new ArrayList<>();

        Thread[] threads = new Thread[threadsCount];
        PrimeNumbersThread[] results = new PrimeNumbersThread[threadsCount];

        for (int i = 0, j = min; i < threadsCount; i++, j += 2) {

            if (j % 2 == 0)
                j++;

            final int from = j;

            //threads[i] = new Thread(() -> FindPrimeNumbersInRange(from, max, 4));
            results[i] = new PrimeNumbersThread(this, from, max, 2 * threadsCount, i);
            threads[i] = new Thread(results[i]);

            threads[i].start();
        }

        for (int i = 0; i < threadsCount; i++) {
            threads[i].join();
            primeNumbers.addAll(results[i].Result);
        }

        return primeNumbers;
    }

    ArrayList<Integer> FindPrimeNumbersInRange(int from, int to, int step, int threadNum) {
        ArrayList<Integer> primeNumbers = new ArrayList<>();

        long start = System.currentTimeMillis();

        for (int i = from; i < to; i += step) {
            if (IsPrime(i)) {
                /*
                try {
                    Thread.sleep(9L * (threadNum + 1));
                } catch (InterruptedException e) {
                    throw new RuntimeException(e);
                }
                */
                //System.out.printf("Found number %d thread: %d %n", i, threadNum);
                primeNumbers.add(i);
            }
        }

        long end = System.currentTimeMillis();

        System.out.println(end - start);
        return primeNumbers;
    }

    private static boolean IsPrime(int num) {
        if (num <= 1)
            return false;

        for (int i = 2; i * i <= num; i++) {
            if (num % i == 0)
                return false;
        }

        return true;
    }
}
