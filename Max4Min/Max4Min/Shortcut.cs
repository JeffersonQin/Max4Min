using System;
using System.IO;
using System.Reflection;

namespace Max4Min
{
    public class Shortcut
    {
        private static string startupShortcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "Max4Min.lnk");

        /// <summary>
        /// 为当前正在运行的程序创建一个快捷方式。<br/>
        /// from: https://blog.walterlv.com/post/create-shortcut-file-using-csharp.html
        /// </summary>
        /// <param name="lnkFilePath">快捷方式的完全限定路径。</param>
        /// <param name="args">快捷方式启动程序时需要使用的参数。</param>
        public static void CreateShortcut(string lnkFilePath, string args = "")
        {
            var shellType = Type.GetTypeFromProgID("WScript.Shell");
            dynamic shell = Activator.CreateInstance(shellType);
            var shortcut = shell.CreateShortcut(lnkFilePath);
            shortcut.TargetPath = Assembly.GetEntryAssembly().Location;
            shortcut.Arguments = args;
            shortcut.WorkingDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            shortcut.Save();
        }

        public static void CreateStartupShortcut()
        {
            CreateShortcut(startupShortcut);
        }

        public static void DeleteStartupShortcut()
        {
            if (File.Exists(startupShortcut)) File.Delete(startupShortcut);
        }
    }
}
