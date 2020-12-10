using System;
using System.Threading;

namespace Reactive.Lessons
{
    public class MyObserver<T> : IObserver<T>
    {
        public void OnCompleted()
        {
            var thread = Thread.CurrentThread.ManagedThreadId.ToString();
            Console.WriteLine($"Observation completed on thread {thread}");
        }

        public void OnError(Exception error)
        {
            var thread = Thread.CurrentThread.ManagedThreadId.ToString();
            Console.WriteLine($"Encountered error: {error} on thread {thread}");
        }

        public void OnNext(T value)
        {
            var thread = Thread.CurrentThread.ManagedThreadId.ToString();
            Console.WriteLine($"Received value: {value} on thread {thread}");
        }
    }
}