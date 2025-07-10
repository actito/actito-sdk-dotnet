using ActitoSdk.Android.Internal;
using ActitoSdk.Scannables.Android.Internal;
using ActitoSdk.Scannables.Core.Events;
using ActitoSdk.Scannables.Core.Internal;
using ActitoSdk.Scannables.Core.Models;
using NativeActito = ActitoSdk.Scannables.Android.Binding.ActitoScannablesCompat;

namespace ActitoSdk.Scannables.Android;

public class ActitoScannablesPlatformAndroid : IActitoScannablesPlatform
{
    private Binding.IActitoScannables.IScannableSessionListener? _scannableSessionListener;

    public void Initialize()
    {
        ObserveSession();
    }
    
    public event EventHandler<ActitoScannableDetectedEventArgs>? ScannableDetected;
    
    public event EventHandler<ActitoScannableSessionFailedEventArgs>? ScannableSessionFailed;

    public bool CanStartNfcScannableSession => NativeActito.CanStartNfcScannableSession;

    public void StartScannableSession(Activity activity)
    {
        NativeActito.StartScannableSession(activity);
    }

    public void StartNfcScannableSession(Activity activity)
    {
        NativeActito.StartNfcScannableSession(activity);
    }

    public void StartQrCodeScannableSession(Activity activity)
    {
        NativeActito.StartQrCodeScannableSession(activity);
    }

    public async Task<ActitoScannable> FetchAsync(string tag)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Fetch(tag, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var scannable = (Binding.Models.ActitoScannable)result;

        return NativeConverter.FromNativeScannable(scannable);
    }


    private void ObserveSession()
    {
        if (_scannableSessionListener != null)
            NativeActito.RemoveListener(_scannableSessionListener);

        _scannableSessionListener = new ScannableSessionListener(this);
        NativeActito.AddListener(_scannableSessionListener);
    }


    private class ScannableSessionListener : Java.Lang.Object, Binding.IActitoScannables.IScannableSessionListener
    {
        private readonly ActitoScannablesPlatformAndroid _platform;

        internal ScannableSessionListener(ActitoScannablesPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnScannableDetected(Binding.Models.ActitoScannable scannable)
        {
            _platform.ScannableDetected?.Invoke(
                _platform,
                new ActitoScannableDetectedEventArgs(
                    NativeConverter.FromNativeScannable(scannable)
                )
            );
        }

        public void OnScannableSessionError(Java.Lang.Exception error)
        {
            _platform.ScannableSessionFailed?.Invoke(
                _platform,
                new ActitoScannableSessionFailedEventArgs(
                    error.Message
                )
            );
        }
    }
}
