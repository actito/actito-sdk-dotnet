namespace ActitoSdk.Loyalty.Core.Models;

/// <summary>
/// Represents a digital pass issued by Actito.
/// </summary>
/// <remarks>
/// An <see cref="ActitoPass"/> can be used for loyalty programs, coupons, tickets, or other
/// redeemable items. It includes metadata, redemption history, and optional
/// integrations with mobile wallets like Apple Wallet or Google Pay.
/// </remarks>
public class ActitoPass
{
    /// <summary>
    /// Unique identifier of the pass.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Optional type of the pass.
    /// </summary>
    /// <remarks>
    /// Supported pass types:
    /// 
    /// - `boarding`
    /// - `coupon`
    /// - `ticket`
    /// - `generic`
    /// - `card`
    /// </remarks>
    public string? Type { get; }

    /// <summary>
    /// Version of the pass.
    /// </summary>
    public int Version { get; }

    /// <summary>
    /// Optional Apple Wallet passbook URL or identifier.
    /// </summary>
    public string? Passbook { get; }

    /// <summary>
    /// Optional template identifier used to generate the pass.
    /// </summary>
    public string? Template { get; }

    /// <summary>
    /// Serial number of the pass.
    /// </summary>
    public string Serial { get; }

    /// <summary>
    /// Barcode value associated with the pass.
    /// </summary>
    public string Barcode { get; }

    /// <summary>
    /// Redemption behavior of the pass.
    /// </summary>
    /// <remarks>
    /// Supported redemption modes:
    /// 
    /// - `once` — the pass can be redeemed a single time
    /// - `limit` — the pass can be redeemed a limited number of times
    /// - `always` — the pass can be redeemed an unlimited number of times
    /// </remarks>
    public string Redeem { get; }

    /// <summary>
    /// History of past redemptions for this pass.
    /// </summary>
    public IList<ActitoPassRedemption> RedeemHistory { get; }

    /// <summary>
    /// Maximum number of times the pass can be redeemed.
    /// </summary>
    public int Limit { get; }

    /// <summary>
    /// Token associated with the pass for secure validation.
    /// </summary>
    public string Token { get; }

    /// <summary>
    /// Collection of key-value pairs used to add extra information to the pass.
    /// </summary>
    public IDictionary<string, object> Data { get; }

    /// <summary>
    /// Timestamp indicating when the pass was created or issued.
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Optional link to save the pass to Google Pay.
    /// </summary>
    public string? GooglePaySaveLink { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoPass"/>.
    /// </summary>
    public ActitoPass(string id, string? type, int version, string? passbook, string? template, string serial,
        string barcode, string redeem, IList<ActitoPassRedemption> redeemHistory, int limit, string token,
        IDictionary<string, object> data, DateTime date, string? googlePaySaveLink)
    {
        Id = id;
        Type = type;
        Version = version;
        Passbook = passbook;
        Template = template;
        Serial = serial;
        Barcode = barcode;
        Redeem = redeem;
        RedeemHistory = redeemHistory;
        Limit = limit;
        Token = token;
        Data = data;
        Date = date;
        GooglePaySaveLink = googlePaySaveLink;
    }
}

/// <summary>
/// Represents a single redemption record for an Actito pass.
/// </summary>
/// <remarks>
/// Each <see cref="ActitoPassRedemption"/> records the time and optional comments
/// when a pass was redeemed.
/// </remarks>
public class ActitoPassRedemption
{
    /// <summary>
    /// Optional comments associated with the redemption.
    /// </summary>
    public string? Comments { get; }

    /// <summary>
    /// Timestamp when the pass was redeemed.
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoPassRedemption"/>.
    /// </summary>
    public ActitoPassRedemption(string? comments, DateTime date)
    {
        Comments = comments;
        Date = date;
    }
}
