# Contributing

1. Install the .NET 10 SDK on Windows and open `ScoopNest.slnx`.
2. Create a branch for your change and keep changes focused.
3. Run `dotnet build ScoopNest.slnx`. For UI changes, check the affected workflows in the app; use a disposable Scoop installation when testing installation or removal.
4. Describe the change and how you verified it in your pull request. Avoid committing `Data`, downloaded packages, credentials, or build outputs.

The WPF host is in `src/ScoopNest.App`; Scoop logic is in `src/ScoopNest.Scoop`. Contracts and infrastructure are separate projects. Keep platform and process behavior behind existing interfaces where practical.
