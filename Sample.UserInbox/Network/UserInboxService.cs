using System.Net.Http.Headers;
using ActitoSdk;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.IdentityModel.Tokens;

namespace Sample.UserInbox.Network;

public class UserInboxService : ObservableObject
{
    private static readonly HttpClient HttpClient = new();

    internal async Task<string> GetInboxResponse(string token)
    {
        var inboxConfiguration = MauiProgram.GetUserInboxConfiguration();
        var fetchInboxUrl = inboxConfiguration["USER_INBOX_FETCH_INBOX_URL"] ??
                            throw new InvalidOperationException("Missing fetch inbox URL.");

        var request = new HttpRequestMessage(HttpMethod.Get, fetchInboxUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await HttpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch inbox. Invalid response code: {response.StatusCode}");

        return await response.Content.ReadAsStringAsync();
    }

    internal async Task RegisterDeviceWithUser(string token)
    {
        await RegisterDeviceAsync(token, "PUT");
    }

    internal async Task RegisterDeviceAsAnonymous(string token)
    {
        await RegisterDeviceAsync(token, "DELETE");
    }

    private async Task RegisterDeviceAsync(string token, string method)
    {
        if (string.IsNullOrEmpty(method) || !(method == "PUT" || method == "DELETE"))
            throw new ArgumentException("Method must be either PUT or DELETE", nameof(method));

        var device = Actito.Device.CurrentDevice ??
                     throw new InvalidOperationException("Cannot register device without Device ID.");

        var inboxConfiguration = MauiProgram.GetUserInboxConfiguration();
        var registerDeviceUrl = inboxConfiguration["USER_INBOX_REGISTER_DEVICE_URL"] ??
                                throw new InvalidOperationException("Missing register device URL.");


        var request = new HttpRequestMessage(new HttpMethod(method), $"{registerDeviceUrl}/{device.Id}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await HttpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to register device. Invalid response code: {response.StatusCode}");
    }
}
