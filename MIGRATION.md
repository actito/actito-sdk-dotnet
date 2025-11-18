# MIGRATING

Actito 5.x is a complete rebranding of the Notificare SDK. Most of the migration involves updating the implementation from Notificare to Actito while keeping the original method invocations.

## Deprecations

Crash reporting is now deprecated and disabled by default. We recommend using another solution to collect crash analytics.
In case you have explicitly opted in, consider removing the following:
- Android: remove `re.notifica.crash_reports_enabled` meta-data from your `AndroidManifest.xml`.
- iOS: remove `CRASH_REPORTING_ENABLED` from your `ActitoOptions.plist`

## Breaking changes

### Removals

- Removed Scannables module.

### Dependencies

Align the dependency with the new package name.
Keep only the dependencies that you already use in your project.

```diff
- Notificare
- Notificare.Assets
- Notificare.Geo
- Notificare.InAppMessaging
- Notificare.Inbox
- Notificare.Loyalty
- Notificare.Push
- Notificare.PushUI
- Notificare.UserInbox

+ Actito
+ Actito.Assets
+ Actito.Geo
+ Actito.InAppMessaging
+ Actito.Inbox
+ Actito.Loyalty
+ Actito.Push
+ Actito.PushUI
+ Actito.UserInbox
```

### Configuration file

Rename configuration file:
- Android: `notificare-services.json` -> `actito-services.json`
- iOS: `NotificareServices.plist` -> `ActitoServices.plist`, also make sure to align the options file `NotificareOptions.plist` -> `ActitoOptions.plist`

And ensure to align those in your app's .csproj file:

```diff
<ItemGroup Condition="$(TargetFramework.Contains('android'))">
-  <NotificareServicesJson Include="Platforms\Android\notificare-services.json" />
+  <ActitoServicesJson Include="Platforms\Android\actito-services.json" />
</ItemGroup>

<ItemGroup Condition="$(TargetFramework.Contains('ios'))">
-  <BundleResource Include="Platforms\iOS\NotificareServices.plist">
-    <Link>NotificareServices.plist</Link>
-  </BundleResource>
  
+  <BundleResource Include="Platforms\iOS\ActitoServices.plist">
+    <Link>ActitoServices.plist</Link>
+  </BundleResource>
</ItemGroup>
```

### Setup Android

Open your `AndroidManifest.xml` file and rename all `re.notifica` constants to `com.actito`. For instance, here's an example of the changes to the `NotificationActivity`:

```diff
<application>
    <activity
-           android:name="re.notifica.push.ui.NotificationActivity"
+           android:name="com.actito.push.ui.NotificationActivity"
            android:exported="false"
            android:theme="@style/AppTheme.Translucent" />
</application>
```

### Implementation

You must update all references to the old Notificare classes and packages throughout your project.
Replace any class names starting with `Notificare` (for example, `NotificarePush`, `NotificarePushUI`, `NotificareGeo`, etc.) with their Actito equivalents (`ActitoPush`, `ActitoPushUI`, `ActitoGeo`, and so on).

Similarly, update all imports from the `using NotificareSdk.Push` to `using ActitoSdk.Push`.

Here is an example from the inbox implementation:

```diff
- using NotificareSdk.Inbox;
- using NotificareSdk.Inbox.Core.Models;

+ using ActitoSdk.Inbox;
+ using ActitoSdk.Inbox.Core.Models;

- internal void Remove(NotificareInboxItem item)
+ internal void Remove(ActitoInboxItem item)
{
    Task.Run(async () =>
    {
        try
        {
-            await NotificareInbox.RemoveAsync(item);
+            await ActitoInbox.RemoveAsync(item);
        }
        catch (Exception e)
        {
            // Handle error
        }
    });
}
```

> **Tip:**
>
> A global search-and-replace can accelerate this migration, but review your code carefully.

### Overriding Localizable Resources

#### Android

If your project overrides SDK-provided localizable strings or other resources, you must update their names to align with the new Actito namespace.
All resource identifiers previously prefixed with `notificare_` should now use the `actito_` prefix instead.

For example, in your app's project `Platforms/Android/Resources/values-fr/strings.xml` file:

```diff
- <string name="notificare_dialog_cancel_button">Annuler</string>
+ <string name="actito_dialog_cancel_button">Annuler</string>
```

Ensure this change is applied consistently across all localized resource files (for example, values-es, values-fr, etc.) within your res directory.

> **Tip:**
>
> A global search for `notificare_` in your `res/` folder will help you quickly locate and rename all relevant keys to the new `actito_` format.

#### iOS

If your project overrides SDK-provided localizable strings or other resources, you must update their names to align with the new Actito namespace.
All resource identifiers previously prefixed with `notificare_` should now use the `actito_` prefix instead.

For example, in your app's project `Platforms/iOS/Resources/fr.lproj/Localizable.strings` file:

```diff
- notificare_cancel_button = "Annuler";
+ actito_cancel_button = "Annuler";
```

> **Tip:**
>
> A global search for `notificare_` in your localizable folder will help you quickly locate and rename all relevant keys to the new `actito_` format.

### Restricted Tag Naming

Tag naming rules have been tightened to ensure consistency.
Tags added using `Actito.device().addTag()` or `Actito.device().addTags()` must now adhere to the following constraints:

- The tag name must be between 3 and 64 characters in length.
- Tags must start and end with an alphanumeric character.
- Only letters, digits, underscores (`_`), and hyphens (`-`) are allowed within the name.

> **Example:**
>
> ✅ `premium_user`  ✅ `en-GB`  ❌ @user


### Restricted Event Naming and Payload Size

Event naming and payload validation rules have also been standardized.
Custom events logged with `Actito.events().logCustom();` must comply with the following:

- Event names must be between 3 and 64 characters.
- Event names must start and end with an alphanumeric character.
- Only letters, digits, underscores (`_`), and hyphens (`-`) are permitted.
- The event data payload is limited to 2 KB in size. Ensure you are not sending excessively large or deeply nested objects when calling: `Actito.shared.events().logCustom(eventName, data: data)`.

> **Tip:**
>
> To avoid exceeding the payload limit, keep your event data minimal — include only the essential key–value pairs required for personalized content or campaign targeting.
