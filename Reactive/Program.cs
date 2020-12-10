using System;
using Reactive.Lessons;

namespace Reactive
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine("1. Lesson One");
                Console.WriteLine("2. Lesson Two");
                Console.WriteLine("3. Simple Observable");
                Console.WriteLine("4. Wrapper");
                Console.WriteLine("5. Function");
                Console.WriteLine("Q. Quit");
                var key = Console.ReadKey();

                Console.Clear();
                if (key.KeyChar.ToString().ToUpper() == "Q") break;
                Exec(key.KeyChar);
                Console.WriteLine();
            }
        }

        private static void Exec(char keyChar)
        {
            if (keyChar == '1')
            {
                var lesson = new LessonOne();
                lesson.Start();
            }
            else if (keyChar == '2')
            {
                var lesson = new LessonTwo();
                lesson.Start();
            }
            else if (keyChar == '3')
            {
                var lesson = new SimpleObservable();
                lesson.Start();
            }
            else if (keyChar == '4')
            {
                var lesson = new Wrapper();
                lesson.Start();
            }
            else if (keyChar == '5')
            {
                var lesson = new Function();
                lesson.Start();
            }
        }
    }
}
