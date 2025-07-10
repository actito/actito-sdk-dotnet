using ActitoSdk.Scannables.Core.Events;
using ActitoSdk.Scannables.Core.Models;

namespace ActitoSdk.Scannables.Core.Internal;

public interface IActitoScannablesPlatform
{
    void Initialize();

    event EventHandler<ActitoScannableDetectedEventArgs> ScannableDetected;

    event EventHandler<ActitoScannableSessionFailedEventArgs> ScannableSessionFailed;

    bool CanStartNfcScannableSession { get; }
    
#if ANDROID
    void StartScannableSession(Activity activity);
    
    void StartNfcScannableSession(Activity activity);
    
    void StartQrCodeScannableSession(Activity activity);
#elif IOS
    void StartScannableSession(UIViewController controller);
    
    void StartNfcScannableSession();
    
    void StartQrCodeScannableSession(UIViewController controller, bool modal);
#endif

    Task<ActitoScannable> FetchAsync(string tag);
}
