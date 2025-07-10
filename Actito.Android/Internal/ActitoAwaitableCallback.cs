using Java.Lang;
using ActitoSdk.Android.Binding;

namespace ActitoSdk.Android.Internal;

public class ActitoAwaitableCallback : Java.Lang.Object, IActitoCallback
{
    private readonly TaskCompletionSource<Java.Lang.Object?> _taskCompletionSource = new();
    public Task<Java.Lang.Object?> Task => _taskCompletionSource.Task;

    public void OnFailure(Java.Lang.Exception e)
    {
        _taskCompletionSource.TrySetException(Throwable.ToException(e));
    }

    public void OnSuccess(Java.Lang.Object? result)
    {
        _taskCompletionSource.TrySetResult(result);
    }
}
