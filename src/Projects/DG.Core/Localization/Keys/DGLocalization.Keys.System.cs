namespace DeadlyGame.Core.Localization
{
    // ================================ //
    //
    // SYSTEM (KEYS)
    //
    // ================================ //

    public static partial class DGLocalization
    {
        public static string SYSTEM_GAME_TITLE => lSystem.GetSection("game").GetKey("title");
        public static string SYSTEM_GAME_DESCRIPTION => lSystem.GetSection("game").GetKey("description");

        public static string SYSTEM_LANGUAGE_DISPLAY_NAME => lSystem.GetSection("language").GetKey("display_name");
        public static string SYSTEM_LANGUAGE_NAME => lSystem.GetSection("language").GetKey("name");

        public static string SYSTEM_CHOICES_IS_EXCLUSIVE => lSystem.GetSection("choices").GetKey("is_exclusive");
        public static string SYSTEM_CHOICES_MADE_PREVIOUSLY => lSystem.GetSection("choices").GetKey("choice_made_previously");
    }
}