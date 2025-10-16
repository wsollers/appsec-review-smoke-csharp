// Thin wrappers around the few external commands the maintenance verbs need.
// Deliberately vulnerable smoke target; see README.md.
using System.Diagnostics;

namespace Smoke
{
    public static class ShellHelpers
    {
        // Flushes pending writes before a cache-warming run; the command is fixed.
        public static void WarmCache()
        {
            Process.Start("/bin/sh", "-c " + "sync");
        }
    }
}
