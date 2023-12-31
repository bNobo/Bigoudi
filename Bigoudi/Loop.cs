﻿namespace Bigoudi;

public class Loop
{
    public static void ForEach<T>(IEnumerable<T> elements, Action<T> execute)
    {
        elements.ForEach(execute);
    }

    public static void For(Action<int> execute, int length, int start = 0, int increment = 1)
    {
        for (int i = start; i < length; i += increment)
        {
            execute(i);
        }
    }

    public static void Iterate(Action<int> execute, int iterations, int start = 0, int increment = 1)
    {
        int i = start;

        while (iterations > 0) 
        {
            execute(i);
            i += increment;
            iterations--;
        }
    }

    public static void While(Action execute, Func<bool> condition)
    {
        while (condition())
        {
            execute();
        }
    }

    public static void DoWhile(Action execute, Func<bool> condition) 
    {
        do
        {
            execute();
        } while (condition());
    }

    /// <summary>
    /// Use with caution !
    /// </summary>
    public static void Infinite(CancellationToken token, Action? execute = null)
    {
        while (true)
        {
            execute?.Invoke();
            token.ThrowIfCancellationRequested();
        }
    }
}
