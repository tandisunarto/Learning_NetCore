using System;
using System.Reactive.Linq;
using System.Threading;

namespace Reactive.Lessons
{
    public class LessonOne
    {
        public void Start()
        {
            IObservable<int> observable = new MyObservable<int>(6, 9);
            var sub = observable.Subscribe(new MyObserver<int>());
            sub.Dispose();
        }
    }
}