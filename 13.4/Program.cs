
using System.Runtime.CompilerServices;


object locker = new Object();
Thread t1 = new Thread(Identify);
Thread t2 = new Thread(Identify);
Thread t3 = new Thread(Identify);
t1.Start(1);
Thread.Sleep(333);
t2.Start(2);
Thread.Sleep(333);
t3.Start(3);
t1.Join();
t2.Join();
t3.Join();

void Identify(object obj)
{
    int id = (int)obj;
    while (true)
    {
        Console.Write($"{id}-");
        Thread.Sleep(TimeSpan.FromSeconds(1));
    }
}

