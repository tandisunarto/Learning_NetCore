using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Reactive.Lessons
{
    public class Wrapper
    {
        public IObservable<int> GetTaskObservable()
        {
            return GetTask().ToObservable();
        }

        public Task<int> GetTask()
        {
            return Task.FromResult<int>(117);
        }

        public void Start()
        {
            IObservable<int> sequence = GetTaskObservable();

            sequence.Subscribe(
                x => Console.WriteLine("OnNext: {0}", x),
                ex => Console.WriteLine("OnError: {0}", ex),
                () => Console.WriteLine("OnCompleted")
            );
        }
    }
}