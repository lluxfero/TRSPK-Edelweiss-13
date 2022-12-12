using System.Diagnostics;

#region Просто три потока
{
    Thread t1 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 1 started");
        for (int i = 0; i < 10; ++i)
        {
            Console.WriteLine("Thread 1");
            // Чтобы потоки не были слишком быстрыми, 
            // заставляем их затормаживать
            Thread.Sleep(100);
        }
        Console.WriteLine("Thread 1 ended");
    }));

    Thread t2 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 2 started");
        for (int i = 0; i < 10; ++i)
        {
            Console.WriteLine("Thread 2");
            Thread.Sleep(100);
        }
        Console.WriteLine("Thread 2 ended");
    }));

    Thread t3 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 3 started");
        for (int i = 0; i < 10; ++i)
        {
            Console.WriteLine("Thread 3");
            Thread.Sleep(100);
        }
        Console.WriteLine("Thread 3 ended");
    }));

    t1.Start();
    t2.Start();
    t3.Start();

    // Для того, чтобы показать разницу между тредом и бэкграунд тредом
    // не ждём окончания тредов(это безопасно, они умирают сами)
    // а умираем сразу же как это возможно
    //t1.Join();
    //t2.Join();
    //t3.Join();
    Console.WriteLine("Main ended");
}
#endregion


#region Бэкграунд потоки
{
    Thread t1 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 1 started");
        for (int i = 0; i < 10; ++i)
        {
            Console.WriteLine("Thread 1");
            // Чтобы потоки не были слишком быстрыми, 
            // заставляем их затормаживать
            Thread.Sleep(100);
        }
        Console.WriteLine("Thread 1 ended");
    }));
    t1.IsBackground = true;

    Thread t2 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 2 started");
        for (int i = 0; i < 10; ++i)
        {
            Console.WriteLine("Thread 2");
            Thread.Sleep(100);
        }
        Console.WriteLine("Thread 2 ended");
    }));
    t2.IsBackground = true;

    Thread t3 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 3 started");
        for (int i = 0; i < 10; ++i)
        {
            Console.WriteLine("Thread 3");
            Thread.Sleep(100);
        }
        Console.WriteLine("Thread 3 ended");
    }));
    t3.IsBackground = true;

    t1.Start();
    t2.Start();
    t3.Start();

    // Бэкграунд треды заканчиваются вместе с мейном, даже если они ещё не закончили
    //t1.Join();
    //t2.Join();
    //t3.Join();
    Console.WriteLine("Main ended");
}

#endregion


#region С нагрузкой на процессор
{
    // Чтобы считать нагрузку на проц. используется System.Diagnostics.PerfomanceCounter
    // работает он только на Win32
    if ((Environment.OSVersion.Platform & PlatformID.Win32NT) == 0)
    {
        Console.WriteLine("Current project should be run on WIN32!");
        return;
    }

    // Thread.Abort не работает на современных компиляторах
    // поэтому управлять потоками мы будем "такими" переменнами
    bool die = false;
    bool do_delay = false;

    Thread t1 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 1 started");
        while (!die)
        {
            if (do_delay)
            {
                Thread.Sleep(50);
            }
        }

        Console.WriteLine("Thread 1 ended");
    }));
    Thread t2 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 2 started");
        while (!die)
        {
            if (do_delay)
            {
                Thread.Sleep(50);
            }
        }
        Console.WriteLine("Thread 2 ended");
    }));
    Thread t3 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 3 started");
        while (!die)
        {
            if (do_delay)
            {
                Thread.Sleep(50);
            }
        }
        Console.WriteLine("Thread 3 ended");
    }));

    t1.Start();
    t2.Start();
    t3.Start();

    PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

    for (int i = 0; i < 10; i++)
    {
        Thread.Sleep(100);
        string usage = cpu.NextValue() + "%";
        Console.WriteLine($"CPU Usage after 100 ms : {usage}");
    }
    Console.WriteLine("\nKilling threads");
    die = true;
    t1.Join();
    t2.Join();
    t3.Join();
    Thread.Sleep(1000);

    // Добавление паузы в потоки позволяет снизить нагрузку на процессор
    Console.WriteLine("\nAdding 50ms delay to while(true) iteration");
    Console.WriteLine("\'Restarting\' threads");
    t1 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 1 started");
        while (!die)
        {
            if (do_delay)
            {
                Thread.Sleep(50);
            }
        }

        Console.WriteLine("Thread 1 ended");
    }));

    t2 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 2 started");
        while (!die)
        {
            if (do_delay)
            {
                Thread.Sleep(50);
            }
        }
        Console.WriteLine("Thread 2 ended");
    }));

    t3 = new(new ThreadStart(delegate ()
    {
        Console.WriteLine("Thread 3 started");
        while (!die)
        {
            if (do_delay)
            {
                Thread.Sleep(50);
            }
        }
        Console.WriteLine("Thread 3 ended");
    }));

    do_delay = true;
    die = false;

    t1.Start();
    t2.Start();
    t3.Start();
    for (int i = 0; i < 10; i++)
    {
        Thread.Sleep(100);
        string usage = cpu.NextValue() + "%";
        Console.WriteLine($"CPU Usage after 100 ms : {usage}");
    }
    Console.WriteLine("\nKilling threads");
    die = true;
    t1.Join();
    t2.Join();
    t3.Join();
    Console.WriteLine("Main ended");
}
#endregion
