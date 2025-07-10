using ActitoSdk.iOS.Internal;
using ActitoSdk.Loyalty.Core.Models;

namespace ActitoSdk.Loyalty.iOS.Internal;

internal static class NativeConverter
{
    internal static ActitoPass FromNativePass(ActitoSdk.Loyalty.iOS.Binding.ActitoPass pass)
    {
        return new ActitoPass(
            id: pass.PassId,
            type: pass.Type,
            version: pass.Version.ToInt32(),
            passbook: pass.Passbook,
            template: pass.PassTemplate,
            serial: pass.Serial,
            barcode: pass.Barcode,
            redeem: pass.Redeem,
            redeemHistory: pass.RedeemHistory.Select(FromNativePassRedemption).ToList(),
            limit: pass.Limit.ToInt32(),
            token: pass.Token,
            data: ActitoNativeConverter.FromNativeExtraDictionary(pass.Data),
            date: DateTimeOffset.FromUnixTimeSeconds((long)pass.Date.SecondsSince1970).DateTime,
            googlePaySaveLink: null
        );
    }

    private static ActitoPassRedemption FromNativePassRedemption(ActitoSdk.Loyalty.iOS.Binding.ActitoPassRedemption redemption)
    {
        return new ActitoPassRedemption(
            comments: redemption.Comments,
            date: DateTimeOffset.FromUnixTimeSeconds((long)redemption.Date.SecondsSince1970).DateTime
        );
    }

    internal static ActitoSdk.Loyalty.iOS.Binding.ActitoPass ToNativePass(ActitoPass pass)
    {
        return new ActitoSdk.Loyalty.iOS.Binding.ActitoPass(
            passId: pass.Id,
            type: pass.Type,
            version: pass.Version,
            passbook: pass.Passbook,
            passTemplate: pass.Template,
            serial: pass.Serial,
            barcode: pass.Barcode,
            redeem: pass.Redeem,
            redeemHistory: pass.RedeemHistory.Select(ToNativePassRedemption).ToArray(),
            limit: pass.Limit,
            token: pass.Token,
            data: ActitoNativeConverter.ToNativeExtraDictionary(pass.Data),
            date: NSDate.FromTimeIntervalSince1970(new DateTimeOffset(pass.Date).ToUnixTimeSeconds())
        );
    }

    private static ActitoSdk.Loyalty.iOS.Binding.ActitoPassRedemption ToNativePassRedemption(
        ActitoPassRedemption redemption)
    {
        return new ActitoSdk.Loyalty.iOS.Binding.ActitoPassRedemption(
            comments: redemption.Comments,
            date: NSDate.FromTimeIntervalSince1970(new DateTimeOffset(redemption.Date).ToUnixTimeSeconds())
        );
    }
}
