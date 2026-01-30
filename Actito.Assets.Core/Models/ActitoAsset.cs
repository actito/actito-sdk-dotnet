namespace ActitoSdk.Assets.Core.Models;

/// <summary>
/// Represents a rich asset returned by Actito.
/// </summary>
/// <remarks>
/// An <see cref="ActitoAsset"/> contains displayable content such as a title,
/// optional descriptive text, a link to a binary file, and elements like a button
/// or metadata. Additional fields are stored in <see cref="ActitoAsset.extra"/>.
/// </remarks>
public class ActitoAsset
{
    /// <summary>
    ///  The title of the asset.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Optional description of the asset.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Optional key of the asset.
    /// </summary>
    public string? Key { get; }

    /// <summary>
    /// Optional binary file url of the asset.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Optional button associated with the asset.
    /// </summary>
    public ActitoAssetButton? Button { get; }

    /// <summary>
    /// Optional metadata associated with the asset.
    /// </summary>
    public ActitoAssetMetaData? MetaData { get; }

    /// <summary>
    /// Collection of key-value pairs used to add extra information to the asset.
    /// </summary>
    public IDictionary<string, object> Extra { get; }

    /// <summary>
    /// Contructor for <see cref="ActitoAsset"/>.
    /// </summary>
    public ActitoAsset(string title, string? description, string? key, string? url, ActitoAssetButton? button,
        ActitoAssetMetaData? metaData, IDictionary<string, object> extra)
    {
        Title = title;
        Description = description;
        Key = key;
        Url = url;
        Button = button;
        MetaData = metaData;
        Extra = extra;
    }
}

/// <summary>
/// Represents a call-to-action button associated with an <see cref="ActitoAsset"/>.
/// </summary>
public class ActitoAssetButton
{
    /// <summary>
    /// Optional text displayed on the button.
    /// </summary>
    public string? Label { get; }

    /// <summary>
    /// Optional action associated with the button.
    /// </summary>
    public string? Action { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoAssetButton"/>.
    /// </summary>
    public ActitoAssetButton(string? label, string? action)
    {
        Label = label;
        Action = action;
    }
}

/// <summary>
/// Contains metadata describing the underlying file of an <see cref="ActitoAsset"/>.
/// </summary>
public class ActitoAssetMetaData
{
    /// <summary>
    /// The original name of the file as provided at upload time.
    /// </summary>
    public string OriginalFileName { get; }

    /// <summary>
    /// The MIME type of the file.
    /// </summary>
    public string ContentType { get; }

    /// <summary>
    /// The size of the file in bytes.
    /// </summary>
    public int ContentLength { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoAssetMetaData"/>.
    /// </summary>
    public ActitoAssetMetaData(string originalFileName, string contentType, int contentLength)
    {
        OriginalFileName = originalFileName;
        ContentType = contentType;
        ContentLength = contentLength;
    }
}
