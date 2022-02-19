using System.Collections;
using System.Threading.Tasks; 

/// <summary>
/// This class is responsible for exetension for Task 
/// </summary>
public static class TaskExtentions
{
    /// <summary>
    /// Run task anywhere
    /// </summary>
    public static async void WrapErrors(this Task task)
    {
        await task;
    }

    /// <summary>
    /// Convert Task to Coroutine
    /// </summary>
    public static IEnumerator AsCoroutine(this Task task)
    {
        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsFaulted)
        {
            throw task.Exception;
        }
    }
}
