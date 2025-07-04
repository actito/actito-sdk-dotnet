using System.Runtime.Serialization.Json;

namespace Actito.Build.Tasks.ProcessActitoServices;

internal static class ActitoServicesLoader
{
    public static ActitoServices ProcessJson(Stream json)
    {
        var serializer = new DataContractJsonSerializer(typeof(ActitoServices));

        if (serializer.ReadObject(json) is not ActitoServices actitoServices)
            throw new NullReferenceException();

        return actitoServices;
    }
}
