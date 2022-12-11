
using System.Diagnostics;

long sum, cur;
object locker = new object();
Stopwatch timer = new();
for (int i = 1; i <= 100; i++)
{
    sum = 0;
    cur = 1;
    timer.Restart();
    for (int j = 0; j < i; j++)
    {
        Thread thread = new Thread(Add)
        {
            Name = "Thread" + j
        };
        thread.Start();
    }
    Thread.Sleep(1000);
    timer.Stop();
    TimeSpan ts = timer.Elapsed;
    Console.WriteLine($"Потоков: {i} --> Время: {ts.Milliseconds} --> Результат: {sum}");
}

void Add()
{
    while (cur <= 1000000)
    {
        lock (locker)
        {
            if (cur <= 1000000)
            {
                sum += cur;
                cur++;
            }
        }
    }
}
