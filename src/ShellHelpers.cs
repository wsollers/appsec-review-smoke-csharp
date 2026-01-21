// Thin wrappers around the few external commands the maintenance verbs need.
// Deliberately vulnerable smoke target; see README.md.
using System.Diagnostics;
using System.IO;

namespace Smoke
{
    public static class ShellHelpers
    {
        private const string UploadRoot = "/var/lib/smoke/uploads";

        // Flushes pending writes before a cache-warming run; the command is fixed.
        public static void WarmCache()
        {
            Process.Start("/bin/sh", "-c " + "sync");
        }

        // Sets the syslog priority used for maintenance messages; only 0 to 3 are accepted.
        public static bool SetLogLevel(int level)
        {
            if (level >= 0 && level <= 3)
            {
                Process.Start("/bin/sh", "-c logger -p " + level);
                return true;
            }
            return false;
        }

        // Prints the size of one upload. The request is reduced to its file name, so the path
        // stays under the upload root, and stat runs directly with options ended by --.
        public static void StatUpload(string name)
        {
            string safe = Path.GetFileName(name);
            Process.Start("/usr/bin/stat", "--format=%s -- " + Path.Combine(UploadRoot, safe));
        }

        // Shell helper from before the controller existed; nothing calls it.
        private static void LegacyShell(string arg)
        {
            Process.Start("/bin/sh", "-c " + arg);
        }
    }
}
