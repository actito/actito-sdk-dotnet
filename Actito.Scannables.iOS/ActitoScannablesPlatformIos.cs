using ActitoSdk.Scannables.Core.Events;
using ActitoSdk.Scannables.Core.Internal;
using ActitoSdk.Scannables.Core.Models;
using ActitoSdk.Scannables.iOS.Internal;

namespace ActitoSdk.Scannables.iOS;

public class ActitoScannablesPlatformIos : IActitoScannablesPlatform
{
    private InternalActitoScannablesDelegate? _delegate;
    private Binding.ActitoScannablesNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalActitoScannablesDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<ActitoScannableDetectedEventArgs>? ScannableDetected;
    public event EventHandler<ActitoScannableSessionFailedEventArgs>? ScannableSessionFailed;

    public bool CanStartNfcScannableSession => _native.CanStartNfcScannableSession;

    public void StartScannableSession(UIViewController controller)
    {
        _native.StartScannableSession(controller);
    }

    public void StartNfcScannableSession()
    {
        _native.StartNfcScannableSession();
    }

    public void StartQrCodeScannableSession(UIViewController controller, bool modal)
    {
        _native.StartQrCodeScannableSession(controller, modal);
    }

    public Task<ActitoScannable> FetchAsync(string tag)
    {
        TaskCompletionSource<ActitoScannable> completion = new();

        _native.Fetch(
            tag,
            scannable => completion.TrySetResult(NativeConverter.FromNativeScannable(scannable)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }


    private sealed class InternalActitoScannablesDelegate : Binding.ActitoScannablesNativeBindingDelegate
    {
        private readonly ActitoScannablesPlatformIos _platform;

        internal InternalActitoScannablesDelegate(ActitoScannablesPlatformIos platform)
        {
            _platform = platform;
        }

        public override void DidDetectScannable(
            Binding.ActitoScannablesNativeBinding actitoScannables,
            Binding.ActitoScannable scannable
        )
        {
            _platform.ScannableDetected?.Invoke(
                _platform,
                new ActitoScannableDetectedEventArgs(
                    NativeConverter.FromNativeScannable(scannable)
                )
            );
        }

        public override void DidInvalidateScannerSession(
            Binding.ActitoScannablesNativeBinding actitoScannables,
            NSError error
        )
        {
            _platform.ScannableSessionFailed?.Invoke(
                _platform,
                new ActitoScannableSessionFailedEventArgs(
                    error.ToString()
                )
            );
        }
    }
}
