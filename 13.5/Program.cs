
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

long sum;
const int SUMTO = 1000000;
int i = 1;
object locker = new object();
Stopwatch timer = new();
List<Thread> threads = new();


for (i = 1; i <= 100; i++)
{
    sum = 0;   
    for (int j = 0; j < i; j++)
    {
        Thread thread = new(Add);
        thread.Name = $"Thread {j}";
        threads.Add(thread);
    }

    timer.Restart();
    for (int j = 0; j < i; j++)
    {
        threads[j].Start(j); // выполняем все потоки
    }
    /*for (int j = 0; j < i; j++)
    {
        threads[j].Join();
    }
    //foreach (var t in threads) t.Join(); // ждем выполнения всех потоков */
    foreach (var t in threads) t.Join();
    timer.Stop();
    TimeSpan ts = timer.Elapsed;
    Console.WriteLine($"Потоков: {i} --> Время: {ts.Milliseconds} --> Результат: {sum}");
    threads.Clear();
}

void Add(object obj)
{
    int cur = (int)obj * SUMTO / i + 1;
    long local_sum = 0;
    while (cur <= ((int)obj + 1) * SUMTO / i)
    {
        local_sum += cur;
        cur++;
    }
    lock (locker)
    {
        sum += local_sum;
    }
}
