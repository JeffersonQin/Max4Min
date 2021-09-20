using System;
using System.IO;
using System.Diagnostics;

namespace Max4Min
{
    public static class Runtime
    {
        private static string baseDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../"));

        public static void Hook()
        {
            Process.Start(Path.Combine(baseDir, "Max4MinCoreRuntimeHook-x86.exe"));
            if (Environment.Is64BitOperatingSystem)
            {
                Process.Start(Path.Combine(baseDir, "Max4MinCoreRuntimeHook-x64.exe"));
            }
        }

        public static void UnHook()
        {
            Process.Start(Path.Combine(baseDir, "Max4MinCoreRuntimeUnHook-x86.exe"));
            Process.Start("taskkill.exe", "/IM Max4MinCoreRuntimeHook-x86.exe /F");
            Process.Start("taskkill.exe", "/IM Max4MinCoreRuntimeUnHook-x86.exe /F");
            if (Environment.Is64BitOperatingSystem)
            {
                Process.Start(Path.Combine(baseDir, "Max4MinCoreRuntimeUnHook-x64.exe"));
                Process.Start("taskkill.exe", "/IM Max4MinCoreRuntimeHook-x64.exe /F");
                Process.Start("taskkill.exe", "/IM Max4MinCoreRuntimeUnHook-x64.exe /F");
            }
        }
    }
}
