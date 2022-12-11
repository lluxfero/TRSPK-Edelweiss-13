List<Thread> threads = new();

for (int i = 0; i < 10; i++)
{
    Thread thread = new(Test.Increment);
    threads.Add(thread);
}

foreach (var t in threads) t.Start(); // выполняем все потоки
foreach (var t in threads) t.Join(); // ждем выполнения всех потоков 

Console.WriteLine($"Значение равно {Test.Num}");

public class Test
{
    public static int Num { get; set; } = 0;
    static object locker = new();
    public static void Increment()
    {
        lock (locker)
        {
            Num++;
            Console.WriteLine(Num);
        }

    }
}
