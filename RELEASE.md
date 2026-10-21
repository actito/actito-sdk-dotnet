# Release process

## Prerequisites

The scripts in `scripts/` require the Ruby version pinned in `.ruby-version` and the gems in the `Gemfile`.

```shell
rbenv install --skip-existing
bundle install
```

Run every script through Bundler (`bundle exec ruby scripts/<script>.rb`) so it uses the locked gem versions.

## Steps

1. Update the `<Version>` tag in `Directory.Build.props`.
2. Download the XCFrameworks from the CDN.
```shell
bundle exec ruby scripts/download-xcframeworks.rb
```
3. Update all generated bindings.
```shell
bundle exec ruby scripts/update-all-bindings.rb --clean
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
