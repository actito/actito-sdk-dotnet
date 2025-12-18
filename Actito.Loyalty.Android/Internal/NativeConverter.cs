using ActitoSdk.Android.Internal;
using ActitoSdk.Loyalty.Core.Models;

namespace ActitoSdk.Loyalty.Android.Internal;

internal static class NativeConverter
{
    internal static ActitoPass FromNativePass(Binding.Models.ActitoPass pass)
    {
        var type = pass.Type;

        return new ActitoPass(
            id: pass.Id,
            type: type == null ? null : FromNativePassType(type),
            version: pass.Version,
            passbook: pass.Passbook,
            template: pass.Template,
            serial: pass.Serial,
            barcode: pass.Barcode,
            redeem: FromNativePassRedeem(pass.GetRedeem()),
            redeemHistory: pass.RedeemHistory.Select(FromNativePassRedemption).ToList(),
            limit: pass.Limit,
            token: pass.Token,
            data: ActitoNativeConverter.FromNativeExtraDictionary(pass.Data),
            date: DateTimeOffset.FromUnixTimeMilliseconds(pass.Date.Time).DateTime,
            googlePaySaveLink: pass.GooglePaySaveLink
        );
    }

    // TODO: Add a rawValue in the native SDK and use that instead.
    private static string FromNativePassType(Binding.Models.ActitoPass.PassType type)
    {
        if (type == Binding.Models.ActitoPass.PassType.Boarding)
            return "boarding";

        if (type == Binding.Models.ActitoPass.PassType.Coupon)
            return "coupon";

        if (type == Binding.Models.ActitoPass.PassType.Ticket)
            return "ticket";

        if (type == Binding.Models.ActitoPass.PassType.Generic)
            return "generic";

        if (type == Binding.Models.ActitoPass.PassType.Card)
            return "card";

        throw new ArgumentException($"Unknown pass type: {type}");
    }

    // TODO: Add a rawValue in the native SDK and use that instead.
    private static string FromNativePassRedeem(Binding.Models.ActitoPass.Redeem redeem)
    {
        if (redeem == Binding.Models.ActitoPass.Redeem.Once)
            return "once";

        if (redeem == Binding.Models.ActitoPass.Redeem.Limit)
            return "limit";

        if (redeem == Binding.Models.ActitoPass.Redeem.Always)
            return "always";

        throw new ArgumentException($"Unknown pass redeem: {redeem}");
    }

    private static ActitoPassRedemption FromNativePassRedemption(
        Binding.Models.ActitoPass.Redemption redemption
    )
    {
        return new ActitoPassRedemption(
            comments: redemption.Comments,
            date: DateTimeOffset.FromUnixTimeMilliseconds(redemption.Date.Time).DateTime
        );
    }

    internal static Binding.Models.ActitoPass ToNativePass(ActitoPass pass)
    {
        return new Binding.Models.ActitoPass(
            id: pass.Id,
            type: pass.Type == null ? null : ToNativePassType(pass.Type),
            version: pass.Version,
            passbook: pass.Passbook,
            template: pass.Template,
            serial: pass.Serial,
            barcode: pass.Barcode,
            redeem: ToNativePassRedeem(pass.Redeem),
            redeemHistory: pass.RedeemHistory.Select(ToNativePassRedemption).ToList(),
            limit: pass.Limit,
            token: pass.Token,
            data: ActitoNativeConverter.ToNativeExtraDictionary(pass.Data),
            date: new Java.Util.Date(new DateTimeOffset(pass.Date).ToUnixTimeMilliseconds()),
            googlePaySaveLink: pass.GooglePaySaveLink
        );
    }

    // TODO: Add a rawValue in the native SDK and use that instead.
    private static Binding.Models.ActitoPass.PassType ToNativePassType(string type)
    {
        return type switch
        {
            "boarding" => Binding.Models.ActitoPass.PassType.Boarding!,
            "coupon" => Binding.Models.ActitoPass.PassType.Coupon!,
            "ticket" => Binding.Models.ActitoPass.PassType.Ticket!,
            "generic" => Binding.Models.ActitoPass.PassType.Generic!,
            "card" => Binding.Models.ActitoPass.PassType.Card!,
            _ => throw new ArgumentException($"Unknown pass type: {type}")
        };
    }

    // TODO: Add a rawValue in the native SDK and use that instead.
    private static Binding.Models.ActitoPass.Redeem ToNativePassRedeem(string redeem)
    {
        return redeem switch
        {
            "once" => Binding.Models.ActitoPass.Redeem.Once!,
            "limit" => Binding.Models.ActitoPass.Redeem.Limit!,
            "always" => Binding.Models.ActitoPass.Redeem.Always!,
            _ => throw new ArgumentException($"Unknown pass redeem: {redeem}")
        };
    }

    private static Binding.Models.ActitoPass.Redemption ToNativePassRedemption(
        ActitoPassRedemption redemption
    )
    {
        return new Binding.Models.ActitoPass.Redemption(
            comments: redemption.Comments,
            date: new Java.Util.Date(new DateTimeOffset(redemption.Date).ToUnixTimeMilliseconds())
        );
    }
}
