using System;
using IWshRuntimeLibrary;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace DellFanManagementTrayAppWpf
{
    static class Autostart
    {
        const string PRODUCT_NAME = "DellFanManagementTrayAppWpf";

        public static void CreateStartupFolderShortcut()
        {
            WshShellClass wshShell = new WshShellClass();
            IWshRuntimeLibrary.IWshShortcut shortcut;
            string startUpFolderPath =
              Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Create the shortcut
            shortcut =
              (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(
                startUpFolderPath + "\\" +
                PRODUCT_NAME + ".lnk");

            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = "Launch DellFanManagementTrayAppWpf";
            shortcut.Arguments = String.Join(" ", Environment.GetCommandLineArgs().Skip(1));
            shortcut.Save();
        }

        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell32.Shell shell = new Shell32.ShellClass();
            Shell32.Folder folder = shell.NameSpace(pathOnly);
            Shell32.FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                try
                {
                    Shell32.ShellLinkObject link =
                        (Shell32.ShellLinkObject)folderItem.GetLink;
                    return link.Path;
                }
                catch (Exception e)
                {
                    return string.Empty;
                }

            }

            return string.Empty; // Not found
        }

        public static bool CheckStartupFolderShortcutsExists()
        {
            string searchFile = Application.ExecutablePath;
            string startUpFolderPath =
              Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            DirectoryInfo di = new DirectoryInfo(startUpFolderPath);
            FileInfo[] files = di.GetFiles("*.lnk");

            foreach (FileInfo fi in files)
            {
                string shortcutTargetFile = GetShortcutTargetFile(fi.FullName);

                if (shortcutTargetFile.Equals(searchFile,
                      StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static void DeleteStartupFolderShortcut()
        {
            string searchFile = Application.ExecutablePath;
            string startUpFolderPath =
              Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            DirectoryInfo di = new DirectoryInfo(startUpFolderPath);
            FileInfo[] files = di.GetFiles("*.lnk");

            foreach (FileInfo fi in files)
            {
                string shortcutTargetFile = GetShortcutTargetFile(fi.FullName);

                if (shortcutTargetFile.Equals(searchFile,
                      StringComparison.InvariantCultureIgnoreCase))
                {
                    System.IO.File.Delete(fi.FullName);
                }
            }
        }
    }
}
