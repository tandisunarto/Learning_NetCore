using System;
using System.Reactive.Subjects;

namespace Reactive.Lessons
{
    public class LessonTwo
    {
        public void Start()
        {
            var subject = new Subject<int>();

            var subscription = subject.Subscribe(Console.WriteLine);

            subject.OnNext(1);
            subject.OnNext(2);
            
            subscription.Dispose();

            subject.OnNext(3);
        }
    }
}