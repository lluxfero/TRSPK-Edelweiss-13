List<Thread> threads = new();
//Random rnd = new((int)DateTime.Now.Ticks);
//Test.sleeptime = rnd.Next(1000, 10000);


for (int i = 0; i < 10; i++)
{
    Thread thread = new(Test.WaitSignal);
    thread.Name = $"№{i}";
    threads.Add(thread);
}

foreach (var t in threads) t.Start(); // выполняем все потоки
Thread.Sleep(1000);
Test.sem.Release(10);
//Test.Signal = true;
foreach (var t in threads) t.Join(); // ждем выполнения всех потоков 

public class Test
{
    public static bool Signal { get; set; } = false;
    public static int sleeptime = 0;
    public static Semaphore sem = new(0, 10);
    public static void WaitSignal()
    {
        long t = 0;
        sem.WaitOne();
        //while (!Signal) 
            //Thread.Sleep(1000); // гибрид блокирования и ожидания в цикле
        Console.WriteLine($"поток {Thread.CurrentThread.Name} завершается");
        Random rnd = new((int)DateTime.Now.Ticks); 
        Thread.Sleep(rnd.Next(1000, 10000));
        //Thread.Sleep(sleeptime);
        t = DateTime.Now.Ticks;
        Console.WriteLine($"поток {Thread.CurrentThread.Name} завершился в {t}");
    }
}
