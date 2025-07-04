using System.Runtime.Serialization;

namespace Actito.Build.Tasks.ProcessActitoServices;

[DataContract]
[Serializable]
internal class ActitoServices
{
    [DataMember(Name = "project_info")] public required ActitoServicesProjectInfo ProjectInfo { get; set; }
    [DataMember(Name = "hosts_info")] public ActitoServicesHostsInfo? HostsInfo { get; set; }
}

[DataContract]
[Serializable]
internal class ActitoServicesProjectInfo
{
    [DataMember(Name = "application_id")] public required string ApplicationId { get; set; }

    [DataMember(Name = "application_key")] public required string ApplicationKey { get; set; }

    [DataMember(Name = "application_secret")]
    public required string ApplicationSecret { get; set; }
}

[DataContract]
[Serializable]
internal class ActitoServicesHostsInfo
{
    [DataMember(Name = "rest_api")] public required string RestApi { get; set; }
    [DataMember(Name = "app_links")] public required string AppLinks { get; set; }
    [DataMember(Name = "short_links")] public required string ShortLinks { get; set; }
}
