using System;
using System.Reactive.Linq;

namespace Reactive.Lessons
{
    public class SimpleObservable
    {
        public IObservable<int> GetSimpleObservable()
        {
            return Observable.Return(117);
        }

        public void Start()
        {
            IObservable<int> sequence = GetSimpleObservable();

            sequence.Subscribe(
                x => Console.WriteLine("OnNext: {0}", x),
                ex => Console.WriteLine("OnError: {0}", ex),
                () => Console.WriteLine("OnCompleted")
            );
        }
    }
}