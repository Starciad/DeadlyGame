using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DG.Settings
{
    internal sealed class DGIniReader
    {
        private readonly string _filename;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        internal DGIniReader(string filename)
        {
            this._filename = new FileInfo(filename).FullName;
        }

        internal string Read(string section, string key)
        {
            StringBuilder retVal = new(255);
            _ = GetPrivateProfileString(section, key, string.Empty, retVal, 255, this._filename);
            return retVal.ToString();
        }
    }
}
