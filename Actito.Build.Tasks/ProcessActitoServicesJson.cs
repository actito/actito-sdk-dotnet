using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Actito.Build.Tasks.ProcessActitoServices;
using Task = Microsoft.Build.Utilities.Task;

namespace Actito.Build.Tasks;

public class ProcessActitoServicesJson : Task
{
    [Required] public required ITaskItem ResStringsPath { get; set; }
    [Required] public required ITaskItem StampPath { get; set; }
    [Required] public required ITaskItem[] ActitoServicesJsonPaths { get; set; }
    [Required] public required ITaskItem ResPath { get; set; }

    [Output] public required ITaskItem[] ResPathAbsolute { get; set; }

    public override bool Execute()
    {
        var resPath = ResPath.GetMetadata("FullPath");
        var stampPath = StampPath.GetMetadata("FullPath");
        var resValuesPath = ResStringsPath.GetMetadata("FullPath");

        // Return the absolute path of our resources dir
        ResPathAbsolute = [new TaskItem(resPath)];

        if (ActitoServicesJsonPaths.Length == 0)
        {
            Log.LogWarning(
                "ActitoServicesJson not defined, make sure to manually configure Actito or set the ActitoServicesJson."
            );

            DeleteFiles(resValuesPath);
            return true;
        }

        if (ActitoServicesJsonPaths.Length > 1)
        {
            Log.LogWarning(
                "Multiple ActitoServicesJson files defined, continuing with the first one."
            );
        }

        var actitoServicesJson = ActitoServicesJsonPaths.First();
        var actitoServicesJsonPath = CleanPath(actitoServicesJson.ItemSpec);

        ActitoServices actitoServices;

        try
        {
            using (var actitoServicesFileStream = File.OpenRead(actitoServicesJsonPath))
                actitoServices = ActitoServicesLoader.ProcessJson(actitoServicesFileStream);

            if (actitoServices == null)
                throw new NullReferenceException();
        }
        catch (Exception ex)
        {
            Log.LogError(
                $"Failed to Read or Deserialize ActitoServicesJson file: {actitoServicesJsonPath}{Environment.NewLine}{ex}"
            );

            DeleteFiles(resValuesPath);
            return false;
        }

        var resItems = new Dictionary<string, string>
        {
            { "actito_services_application_id", actitoServices.ProjectInfo.ApplicationId },
            { "actito_services_application_key", actitoServices.ProjectInfo.ApplicationKey },
            { "actito_services_application_secret", actitoServices.ProjectInfo.ApplicationSecret }
        };

        if (resItems.Any(kvp => string.IsNullOrEmpty(kvp.Value)))
        {
            Log.LogWarning("Some of ActitoServicesJson required entries are missing or empty.");

            DeleteFiles(resValuesPath);
            return false;
        }

        if (actitoServices.HostsInfo != null)
        {
            resItems["actito_services_hosts_rest_api"] = actitoServices.HostsInfo.RestApi;
            resItems["actito_services_hosts_app_links"] = actitoServices.HostsInfo.AppLinks;
            resItems["actito_services_hosts_short_links"] = actitoServices.HostsInfo.ShortLinks;
        }

        Log.LogMessage($"Writing ActitoServicesJson Resource: {resValuesPath}");
        WriteResourceDoc(resValuesPath, resItems);
        Log.LogMessage($"Success writing ActitoServicesJson Resource: {resValuesPath}");

        var stampTxt = string.Empty;

        if (File.Exists(resValuesPath))
            stampTxt += resValuesPath + Environment.NewLine;

        File.WriteAllText(stampPath, stampTxt);
        Log.LogMessage("ActitoServicesJson successfully added.");

        return true;
    }

    private static void WriteResourceDoc(string path, Dictionary<string, string> resourceValues)
    {
        var pathInfo = new FileInfo(path);

        if (pathInfo.Directory is { Exists: false })
            pathInfo.Directory.Create();

        var xws = new XmlWriterSettings
        {
            Indent = true
        };

        using var sw = File.Create(path);
        using var xw = XmlWriter.Create(sw, xws);
        xw.WriteStartDocument();
        xw.WriteStartElement("resources");

        foreach (var kvp in resourceValues.Where(kvp => !string.IsNullOrEmpty(kvp.Value)))
        {
            xw.WriteStartElement("string");
            xw.WriteAttributeString("name", kvp.Key);
            xw.WriteAttributeString("translatable", "false");
            xw.WriteString(kvp.Value);
            xw.WriteEndElement();
        }

        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
    }

    private void DeleteFiles(params string[] paths)
    {
        if (paths.Length == 0)
            return;

        foreach (var p in paths)
        {
            if (!File.Exists(p))
                continue;
            try
            {
                File.Delete(p);
            }
            catch (Exception ex)
            {
                Log.LogWarning($"Failed to delete file: {p}{Environment.NewLine}{ex}");
            }
        }
    }

    private static string CleanPath(params string[] paths)
    {
        var combined = Path.Combine(paths);
        return combined.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
    }
}
