namespace LilaRent.MobileApp.Extensions;


public static class ContinueWithCastExtension
{
    public static Task<TTo> ContinueWithCast<TFrom, TTo>(this Task<TFrom> task) 
    {
        var taskCompletionSource = new TaskCompletionSource<TTo>();

        Task.Factory.StartNew(async () => await InitializeTaskCompletionSource(task, taskCompletionSource));

        return taskCompletionSource.Task;
    }


    private static async Task InitializeTaskCompletionSource<TFrom, TTo>(Task<TFrom> task, TaskCompletionSource<TTo> taskCompletionSource)
    {
        var res = await task;

        if (res is TTo castedResult)
        {
            taskCompletionSource.SetResult(castedResult);
        }
        else if (res is null)
        {
            taskCompletionSource.SetResult(default(TTo)!);
        }
        else
        {
            taskCompletionSource.SetException(new InvalidCastException($"Cant cast reference of {res.GetType().Name} to {typeof(TTo).Name}"));
        }
    }
}
