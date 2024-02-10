import java.util.Date;


@FunctionalInterface
interface ThrowableRunnable<E extends Exception> {
    void run() throws E;
}

@FunctionalInterface
interface ThrowableSupplier<T, E extends Exception> {
    public abstract T get() throws E;
}

public class Stopwatch {
    static class Response<T> {
        public T Result;

        Date BeginDate;
        Date CompleteDate;
        public long Duration(){
            return CompleteDate.getTime() - BeginDate.getTime();
        }

        public Response(T result, Date beginDate, Date completeDate) {
            Result = result;
            BeginDate = beginDate;
            CompleteDate = completeDate;
        }

        public Response(Date beginDate, Date completeDate) {
            BeginDate = beginDate;
            CompleteDate = completeDate;
        }
    }

    public static <T, E extends Exception> Response<T> Execute(ThrowableSupplier<T, E> function) throws E {

        Date start = new Date(System.currentTimeMillis());
        T res = (T)function.get();
        Date end = new Date(System.currentTimeMillis());

        return new Response<T>(res, start, end);
    }

    public static <E extends Exception> Response<?> Execute(ThrowableRunnable<E> function) throws E {
        Date start = new Date(System.currentTimeMillis());
        function.run();
        Date end = new Date(System.currentTimeMillis());

        return new Response<>(start, end);
    }
}
