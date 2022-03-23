using WaitAllVsWhenAll;

static void WaitAndThrow(int id, int waitInMs)
{
    Console.WriteLine($"{DateTime.UtcNow}: Task {id} started");

    Thread.Sleep(waitInMs);
    throw new CustomException($"Task {id} throwing at {DateTime.UtcNow}");
}

static void WaitAll()
{
    Console.WriteLine("\n\n# Task.WaitAll");

    try
    {
        Task[] taskArray = { Task.Factory.StartNew(() => WaitAndThrow(1, 1000)),
                                 Task.Factory.StartNew(() => WaitAndThrow(2, 2000)),
                                 Task.Factory.StartNew(() => WaitAndThrow(3, 3000)) };

        Task.WaitAll(taskArray);
        Console.WriteLine("This isn't going to happen");
    }
    catch (AggregateException ex)
    {
        foreach (var inner in ex.InnerExceptions)
        {
            Console.WriteLine($"Caught AggregateException in Main at {DateTime.UtcNow}: " + inner.Message);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Caught Exception in Main at {DateTime.UtcNow}: " + ex.Message);
    }

    Console.WriteLine("Done.");
}


static async Task WhenAll()
{
    try
    {
        Console.WriteLine("\n\n# Task.WhenAll");

        Task[] taskArray = { Task.Factory.StartNew(() => WaitAndThrow(1, 1000)),
                                 Task.Factory.StartNew(() => WaitAndThrow(2, 2000)),
                                 Task.Factory.StartNew(() => WaitAndThrow(3, 3000)) };

        await Task.WhenAll(taskArray);
        Console.WriteLine("This isn't going to happen");
    }
    catch (AggregateException ex)
    {
        foreach (var inner in ex.InnerExceptions)
        {
            Console.WriteLine($"Caught AggregateException in Main at {DateTime.UtcNow}: " + inner.Message);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Caught Exception in Main at {DateTime.UtcNow}: " + ex.Message);
    }

    Console.WriteLine("Done.");
}


Console.WriteLine("###### Task.WaitAll vs Task.WhenAll ######");

WaitAll();

Task.Run(async () =>
{
    await WhenAll();
}).Wait();

Console.ReadLine();