# ScoopNest

A Windows desktop application for managing Scoop packages, buckets and portable apps.

## Requirements

Windows and the .NET 10 SDK to build. Framework-dependent builds require the matching .NET 10 Windows Desktop Runtime. Scoop can be configured from the application.

## Build and run

Open `ScoopNest.slnx` in an IDE with .NET 10 support, or run:

```powershell
dotnet build ScoopNest.slnx
dotnet run --project src/ScoopNest.App
```

To publish Windows x64 builds, run one of these scripts from PowerShell:

```powershell
./build-normal.ps1
./build-portable.ps1
./build-portable-with-runtimes.ps1
```

The scripts write to separate directories under `builds/`. Normal is a framework-dependent multi-file build; portable is a framework-dependent single-file build; portable with runtimes is a self-contained single-file build. Framework-dependent builds require the .NET 10 Windows Desktop Runtime.

The app stores portable data in a `Data` directory alongside the executable. Keep this directory writable and back it up before moving the app.

## Source layout

- `src/ScoopNest.App`: WPF interface and application host.
- `src/ScoopNest.Scoop`: Scoop operations and package management.
- `src/ScoopNest.Contracts`: interfaces and workspace models.
- `src/ScoopNest.Infrastructure`: settings and platform infrastructure.

See [CONTRIBUTING.md](CONTRIBUTING.md) for contribution instructions.
