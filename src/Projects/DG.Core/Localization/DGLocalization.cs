using DeadlyGame.Core.Serializers.Ini;

using System;
using System.IO;
using System.Text;

namespace DeadlyGame.Core.Localization
{
    internal static partial class DGLocalization
    {
        internal static string DefinedLanguage => definedLanguage;
        internal static string DefinedLanguageRegion => definedLanguageRegion;

        private static string definedLanguage;
        private static string definedLanguageRegion;

        private static DGIni lSystem;

        internal static void Initialize(string language, string region)
        {
            // =============================== //
            // DEFINE

            definedLanguage = language;
            definedLanguageRegion = region;

            // =============================== //
            // LOAD

            string langDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "localization", GetNameOfCurrentDefinedLanguage());

            string langSystemFile = Path.Combine(langDirectory, $"system.ini");

            lSystem = DGIniSerializer.Deserialize(File.ReadAllText(langSystemFile, Encoding.UTF8));
        }

        internal static string GetNameOfCurrentDefinedLanguage()
        {
            return string.Concat(definedLanguage, '-', definedLanguageRegion);
        }

        internal static string Read(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}