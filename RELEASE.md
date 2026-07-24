# Release process

1. Update the `<Version>` tag in `Directory.Build.props`.
2. Download the XCFrameworks from the CDN.
```shell
ruby scripts/download-xcframeworks.rb
```
3. Update all generated bindings.
```shell
ruby scripts/update-all-bindings.rb --clean
```
4. Update the `CHANGELOG.md`.
5. Build the NuGet packages.
```shell
dotnet pack -o packages/x.y.z
```
6. Release the libraries.
```shell
dotnet nuget push "packages/x.y.z/*.nupkg" -k "..." -s https://api.nuget.org/v3/index.json
```
7. Create a GitHub release with the contents of the `CHANGELOG.md`.
