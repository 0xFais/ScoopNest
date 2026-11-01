$ErrorActionPreference = 'Stop'
$project = Join-Path $PSScriptRoot 'src/ScoopNest.App/ScoopNest.App.csproj'
$output = Join-Path $PSScriptRoot 'builds/portable-with-runtimes'
dotnet publish $project -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true --source https://api.nuget.org/v3/index.json -o $output
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

