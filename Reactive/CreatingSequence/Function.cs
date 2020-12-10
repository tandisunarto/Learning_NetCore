using System;
using System.Reactive.Linq;

namespace Reactive.Lessons
{
    public class Function
    {
        public IObservable<long> GetIntervalObservable()
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(1000));
        }

        public void Start()
        {
            IObservable<long> sequence = GetIntervalObservable();

            sequence.Subscribe(
                x => Console.WriteLine("OnNext: {0}", x),
                ex => Console.WriteLine("OnError: {0}", ex),
                () => Console.WriteLine("OnCompleted")
            );
        }
    }
}