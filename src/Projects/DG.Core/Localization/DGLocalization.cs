using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DeadlyGame.Core.Localization
{
    internal static class DGLocalization
    {
        private static string Path;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        internal static void Initialize(string filename)
        {
            Path = new FileInfo(filename).FullName;
        }

        internal static string Read(string section, string key)
        {
            StringBuilder RetVal = new(255);
            _ = GetPrivateProfileString(section, key, string.Empty, RetVal, 255, Path);
            return RetVal.ToString();
        }
    }
}
