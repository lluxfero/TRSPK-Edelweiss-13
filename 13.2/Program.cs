using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();
var signal = new Semaphore(0, 4);

ThreadPool.QueueUserWorkItem(delegate
{
    Console.WriteLine("Initialized work 1\n Time(ms) Elapsed:{0}", sw.ElapsedMilliseconds);
    signal.WaitOne();
    Console.WriteLine("Started work 1\n Time {0}", sw.ElapsedMilliseconds);
});

ThreadPool.QueueUserWorkItem(delegate
{
    Console.WriteLine("Initialized work 2\n Time(ms) Elapsed:{0}", sw.ElapsedMilliseconds);
    signal.WaitOne();
    Console.WriteLine("Started work 2\n Time {0}", sw.ElapsedMilliseconds);
});

ThreadPool.QueueUserWorkItem(delegate
{
    Console.WriteLine("Initialized work 3\n Time(ms) Elapsed:{0}", sw.ElapsedMilliseconds);
    signal.WaitOne();
    Console.WriteLine("Started work 3\n Time {0}", sw.ElapsedMilliseconds);
});

ThreadPool.QueueUserWorkItem(delegate
{
    Console.WriteLine("Initialized work 4\n Time(ms) Elapsed:{0}", sw.ElapsedMilliseconds);
    signal.WaitOne();
    Console.WriteLine("Started work 4\n Time {0}", sw.ElapsedMilliseconds);
});
// Гарантированное время запуска всех потоков
Thread.Sleep(50);
// Посылаем сигнал всем потокам проснуться
for (int i = 0; i < 4; i++)
{
    signal.Release();
}

Thread.Sleep(100);

Console.WriteLine("Main ended");