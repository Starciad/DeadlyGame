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

        private static DGIni lItems;
        private static DGIni lStatements;
        private static DGIni lMessagesBehaviors;

        public static void Initialize(string language, string region)
        {
            // =============================== //
            // DEFINE

            definedLanguage = language;
            definedLanguageRegion = region;

            // =============================== //
            // LOAD

            string langDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "localization", GetNameOfCurrentDefinedLanguage());

            string lItemsFile = Path.Combine(langDirectory, "items.ini");
            string lStatementsFile = Path.Combine(langDirectory, "statements.ini");
            string lMessagesBehaviorsFile = Path.Combine(langDirectory, "messages", "behaviors.ini");

            lItems = DGIniSerializer.Deserialize(File.ReadAllText(lItemsFile, Encoding.UTF8));
            lStatements = DGIniSerializer.Deserialize(File.ReadAllText(lStatementsFile, Encoding.UTF8));
            lMessagesBehaviors = DGIniSerializer.Deserialize(File.ReadAllText(lMessagesBehaviorsFile, Encoding.UTF8));
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