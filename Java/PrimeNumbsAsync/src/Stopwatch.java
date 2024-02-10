import java.util.Date;
import java.util.function.Supplier;

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

    public static <T> Response<T> Execute(Supplier<T> function) {

        Date start = new Date(System.currentTimeMillis());
        T res = (T)function.get();
        Date end = new Date(System.currentTimeMillis());

        return new Response<T>(res, start, end);
    }

    public static Response<?> Execute(Runnable function) {
        Date start = new Date(System.currentTimeMillis());
        function.run();
        Date end = new Date(System.currentTimeMillis());

        return new Response<>(start, end);
    }
}
