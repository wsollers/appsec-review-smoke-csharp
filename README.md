# appsec-review-smoke-csharp

A deliberately vulnerable **smoke target** for [appsec-review](https://github.com/wsollers/appsec-review).
It exists so that a review of a tiny, fully understood C# program has one correct answer at every
stage. **Do not deploy, run against untrusted input, or reuse any of this code.**

The program is a toy ASP.NET Core service: a maintenance controller dispatches operator verbs,
and `src/Program.cs` is the code-graph fixture program with its items controller.

## Build

One .NET project (`Smoke.csproj`, `net8.0`, Web SDK), no network, no generated code. Restore
resolves only against the vendored package feed under `nuget/` (see `nuget.config`) and is held to
`packages.lock.json`:

```sh
dotnet restore --locked-mode && dotnet build --no-restore
```

The build is expected to succeed in the appsec-review `dotnet` build image (.NET SDK 8.0.131) as
an unprivileged user with networking disabled; intermediate and output paths can be redirected
with `-p:BaseIntermediateOutputPath=... -p:OutputPath=...` when the tree is read-only.

## History

`tools/make-history.sh` rebuilds this repository's commit graph from `tools/history/steps/` with
fixed authors and dates, so the history is reproducible and can be reviewed as code.

## License

MIT; see `LICENSE`. Vendored NuGet packages under `nuget/` keep their own licenses.
