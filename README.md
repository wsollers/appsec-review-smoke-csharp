# appsec-review-smoke-csharp

A deliberately vulnerable **smoke target** for [appsec-review](https://github.com/wsollers/appsec-review).
It exists so that a review of a tiny, fully understood C# program has one correct answer at every
stage. **Do not deploy, run against untrusted input, or reuse any of this code.**

The program is a toy ASP.NET Core service: a maintenance controller dispatches operator verbs,
and `src/Program.cs` is the code-graph fixture program with its items controller.

## License

MIT; see `LICENSE`. Vendored NuGet packages under `nuget/` keep their own licenses.
