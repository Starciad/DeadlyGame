using DeadlyGame.Core.Serializers.Ini;

using System;
using System.IO;
using System.Text;

namespace DeadlyGame.Core.Localization
{
    public static partial class DGLocalization
    {
        public static string DefinedLanguage => definedLanguage;
        public static string DefinedLanguageRegion => definedLanguageRegion;

        private static string definedLanguage;
        private static string definedLanguageRegion;

        private static DGIni lSystem;

        public static void Initialize(string language, string region)
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

        public static string GetNameOfCurrentDefinedLanguage()
        {
            return string.Concat(definedLanguage, '-', definedLanguageRegion);
        }

        public static string Read(string v1, string v2)
        {
            return string.Empty;
        }
    }
}