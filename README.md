# waitall-vs-whenall-dotnet

What is the difference between Task.WaitAll and Task.WhenAll?

# Execution

`Task.WaitAll` blocks the current thread until everything has completed.

`Task.WhenAll` returns a *task* which represents the action of waiting until everything has completed.

# Exceptions

`Task.WaitAll` throws an `AggregateException` when any of the tasks throws and you can examine all thrown exceptions. The `await` in `await Task.WhenAll` unwraps the `AggregateException` and 'returns' only the first exception. In both cases, all tasks will run, even if one of them throws an exception.

When the program below executes with `await Task.WhenAll(taskArray)` the output is as follows.

```csharp
23/03/2022 10:18:51: Task 1 started
23/03/2022 10:18:51: Task 2 started
23/03/2022 10:18:51: Task 3 started
Caught AggregateException in Main at 23/03/2022 10:18:54: Task 1 throwing at 23/03/2022 10:18:52
Caught AggregateException in Main at 23/03/2022 10:18:54: Task 2 throwing at 23/03/2022 10:18:53
Caught AggregateException in Main at 23/03/2022 10:18:54: Task 3 throwing at 23/03/2022 10:18:54
Done.
```

When the program below is executed with `Task.WaitAll(taskArray)` the output is as follows.

```csharp
23/03/2022 10:18:54: Task 1 started
23/03/2022 10:18:54: Task 3 started
23/03/2022 10:18:54: Task 2 started
Caught Exception in Main at 23/03/2022 10:18:57: Task 1 throwing at 23/03/2022 10:18:55
Done.
```

# Credits

[c# - WaitAll vs WhenAll - Stack Overflow](https://stackoverflow.com/questions/6123406/waitall-vs-whenall)
