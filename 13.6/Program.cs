List<Thread> threads = new();

for (int i = 0; i < 10; i++)
{
    Thread thread = new(Test.WaitSignal);
    thread.Name = $"№{i}";
    threads.Add(thread);
}

foreach (var t in threads) t.Start(); // выполняем все потоки
Test.Signal = true;
foreach (var t in threads) t.Join(); // ждем выполнения всех потоков 

public class Test
{
    public static bool Signal { get; set; } = false;
    public static void WaitSignal()
    {
        while (!Signal) Thread.Sleep(1000); // гибрид блокирования и ожидания в цикле
        Console.WriteLine($"поток {Thread.CurrentThread.Name} завершается");
        Random rnd = new((int)DateTime.Now.Ticks);
        Thread.Sleep(rnd.Next(1000, 10000));
    }
}
