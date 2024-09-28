namespace DeadlyGame.Core.Localization
{
    // ================================ //
    //
    // SYSTEM (KEYS)
    //
    // ================================ //

    internal static partial class DGLocalization
    {
        internal static string SYSTEM_GAME_TITLE => lSystem.GetSection("game").GetKey("title");
        internal static string SYSTEM_GAME_DESCRIPTION => lSystem.GetSection("game").GetKey("description");

        internal static string SYSTEM_LANGUAGE_DISPLAY_NAME => lSystem.GetSection("language").GetKey("display_name");
        internal static string SYSTEM_LANGUAGE_NAME => lSystem.GetSection("language").GetKey("name");

        internal static string SYSTEM_CHOICES_IS_EXCLUSIVE => lSystem.GetSection("choices").GetKey("is_exclusive");
        internal static string SYSTEM_CHOICES_MADE_PREVIOUSLY => lSystem.GetSection("choices").GetKey("choice_made_previously");
    }
}