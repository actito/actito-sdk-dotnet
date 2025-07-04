namespace ActitoSdk.Assets.Core.Models;

public class ActitoAsset
{
    public string Title { get; }
    public string? Description { get; }
    public string? Key { get; }
    public string? Url { get; }
    public ActitoAssetButton? Button { get; }
    public ActitoAssetMetaData? MetaData { get; }
    public IDictionary<string, object> Extra { get; }

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

public class ActitoAssetButton
{
    public string? Label { get; }
    public string? Action { get; }

    public ActitoAssetButton(string? label, string? action)
    {
        Label = label;
        Action = action;
    }
}

public class ActitoAssetMetaData
{
    public string OriginalFileName { get; }
    public string ContentType { get; }
    public int ContentLength { get; }

    public ActitoAssetMetaData(string originalFileName, string contentType, int contentLength)
    {
        OriginalFileName = originalFileName;
        ContentType = contentType;
        ContentLength = contentLength;
    }
}
