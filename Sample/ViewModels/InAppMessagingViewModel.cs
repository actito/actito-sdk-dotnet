using CommunityToolkit.Mvvm.ComponentModel;
using ActitoSdk.InAppMessaging;

namespace Sample.ViewModels;

public partial class InAppMessagingViewModel : ObservableObject
{
    [ObservableProperty] private bool _evaluateContext;
    [ObservableProperty] private bool _suppressed;

    public InAppMessagingViewModel()
    {
        Suppressed = ActitoInAppMessaging.HasMessagesSuppressed;
    }

    partial void OnSuppressedChanged(bool value)
    {
        SetMessagesSuppressed(value);
    }

    private void SetMessagesSuppressed(bool  suppressed)
    {
      ActitoInAppMessaging.SetMessagesSuppressed(suppressed, EvaluateContext);
    }
}
