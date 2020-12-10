using System;
using System.Reactive.Linq;

namespace Reactive.Lessons
{
    public class MyObservable<T> : IObservable<T>
    {
        private static int _start;
        private static int _range;

        public MyObservable(int start, int range)
        {
            _start = start;
            _range = range;
        }

        private IObservable<T> instance = (IObservable<T>)Observable.Range(_start, _range);

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return instance.Subscribe(observer);
        }
    }
}