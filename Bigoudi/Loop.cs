namespace Bigoudi;

public class Loop
{
    public static void ForEach<T>(IEnumerable<T> elements, Action<T> execute)
    {
        elements.ForEach(execute);
    }
}
