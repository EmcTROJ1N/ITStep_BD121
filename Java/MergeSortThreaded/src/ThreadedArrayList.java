import java.lang.reflect.Array;
import java.util.*;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.atomic.AtomicInteger;

class MergeArraySortThread<T> implements Runnable {
    public List<T> Result;
    private final ThreadedArrayList<T> SourceList;
    private final List<T> ListForSorting;
    Comparator<? super T> _Comparator;

    public MergeArraySortThread(ThreadedArrayList<T> sourceList, List<T> listForSorting, Comparator<? super T> comparator) {
        SourceList = sourceList;
        ListForSorting = listForSorting;
        _Comparator = comparator;
    }

    @Override
    public void run() {
        Result = SourceList.sortArray(ListForSorting, _Comparator);
    }
}

public class ThreadedArrayList<T> extends ArrayList<T> {

    //private AtomicInteger _threadsCount = new AtomicInteger(1);
    //private AtomicInteger _usedThreadsCount = new AtomicInteger(1);

    private int _threadsCount = 1;
    private int _usedThreadsCount = 1;

    public void setThreadsCount(int cnt) {
        _threadsCount = cnt;
    }

    @Override
    public void sort(Comparator<? super T> c) {

        int partArraySize = size() / _threadsCount;
        var parts = new ArrayList<List<T>>();
        for (int i = 0; i < _threadsCount; i++) {
            parts.add(subList(partArraySize * i, partArraySize * (i + 1)));
        }

        if (size() % _threadsCount != 0) {
            parts.removeLast();
            parts.add(subList(partArraySize * _threadsCount - 1, size()));
        }


        var results = new ArrayList<MergeArraySortThread<T>>();
        var threads = new ArrayList<Thread>();
        var sorted = new ArrayList<List<T>>();

        for (var part: parts) {
            var result = new MergeArraySortThread<>(this, part, c);
            var thread = new Thread(result);

            threads.add(thread);
            results.add(result);

            thread.start();
        }

        for (var thread: threads) {
            try {
                thread.join();
            } catch (InterruptedException ex) {
                throw new RuntimeException(ex);
            }
        }

        for (var result: results) {
            sorted.add(result.Result);
        }

        this.clear();
        switch (_threadsCount) {
            case 1:
                this.addAll(sorted.getFirst());
                break;
            case 2:
                this.addAll(mergeArray(sorted.getFirst(), sorted.getLast(), c));
                break;
            case 4:
                this.addAll(mergeArray(
                        mergeArray(sorted.get(0), sorted.get(1), c),
                        mergeArray(sorted.get(2), sorted.get(3), c),
                        c));
                break;
            case 8:
                this.addAll(mergeArray(
                        mergeArray(mergeArray(sorted.get(0), sorted.get(1), c),
                                   mergeArray(sorted.get(2), sorted.get(3), c), c),
                        mergeArray(mergeArray(sorted.get(4), sorted.get(5), c),
                                mergeArray(sorted.get(6), sorted.get(7), c), c),
                        c));
                break;

        }
        /*
        var sorted = sortArray(this, c);
        this.clear();
        this.addAll(sorted);
        */
    }
    private List<T> mergeArray(List<T> arrayA, List<T> arrayB, Comparator<? super T> c) {
        List<T> arrayC = new ArrayList<>();
        int positionA = 0, positionB = 0;

        for (int i = 0; i < arrayA.size() + arrayB.size(); i++) {
            if (positionA == arrayA.size()) {
                arrayC.add(arrayB.get(positionB));
                positionB++;
            } else if (positionB == arrayB.size()) {
                arrayC.add(arrayA.get(positionA));
                positionA++;
            } else if (c.compare(arrayA.get(positionA), arrayB.get(positionB)) > 0) {
                //arrayA[positionA] < arrayB[positionB]) {
                arrayC.add(arrayA.get(positionA));
                positionA++;
            } else {
                arrayC.add(arrayB.get(positionB));
                positionB++;
            }
        }
        return arrayC;
    }
    List<T> sortArray(List<T> arrayA, Comparator<? super T> c) {
        if (arrayA.size() < 2)
            return arrayA;

        List<T> arrayB = arrayA.subList(0, arrayA.size() / 2);
        List<T> arrayC = arrayA.subList(arrayA.size() / 2, arrayA.size());

        // 1 - 2, 2 - 4, 3 - 8, 4 - 16
        arrayB = sortArray(arrayB, c);
        arrayC = sortArray(arrayC, c);
        return mergeArray(arrayB, arrayC, c);


/*    private List<T> mergeArray(List<T> arrayA, List<T> arrayB, Comparator<? super T> c) {
        List<T> arrayC = new ArrayList<>();
        int positionA = 0, positionB = 0;

        for (int i = 0; i < arrayA.size() + arrayB.size(); i++) {
            if (positionA == arrayA.size()) {
                arrayC.add(arrayB.get(positionB));
                positionB++;
            } else if (positionB == arrayB.size()) {
                arrayC.add(arrayA.get(positionA));
                positionA++;
            } else if (c.compare(arrayA.get(positionA), arrayB.get(positionB)) > 0) {
                //arrayA[positionA] < arrayB[positionB]) {
                arrayC.add(arrayA.get(positionA));
                positionA++;
            } else {
                arrayC.add(arrayB.get(positionB));
                positionB++;
            }
        }
        return arrayC;
    }
    List<T> sortArray(List<T> arrayA, Comparator<? super T> c, int depth) {
        if (arrayA.size() < 2)
            return arrayA;

        List<T> arrayB = arrayA.subList(0, arrayA.size() / 2);
        List<T> arrayC = arrayA.subList(arrayA.size() / 2, arrayA.size());

        // 1 - 2, 2 - 4, 3 - 8, 4 - 16
        if (Math.pow(2, depth) <= _threadsCount.get() && _usedThreadsCount.get() < _threadsCount.get()) {

            MergeArraySortThread<T>[] results = new MergeArraySortThread[] {
                    new MergeArraySortThread<>(this, arrayB, ++depth, c),
                    new MergeArraySortThread<>(this, arrayC, ++depth, c)
            };
            Thread[] threads = {
                new Thread(results[0]),
                new Thread(results[1])
            };

            threads[0].start();
            threads[1].start();
            _usedThreadsCount.set(_usedThreadsCount.get() + 2);

            try {
                threads[0].join();
                threads[1].join();
                _usedThreadsCount.set(_usedThreadsCount.get() - 2);
            } catch (InterruptedException ex) {
                throw new RuntimeException(ex);
            }

            return mergeArray(results[0].Result, results[1].Result, c);
        }
        else {
            arrayB = sortArray(arrayB, c, ++depth);
            arrayC = sortArray(arrayC, c, ++depth);
            return mergeArray(arrayB, arrayC, c);
        }
    */}
}
