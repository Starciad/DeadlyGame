namespace DeadlyGame.Core.Localization
{
    // ================================ //
    //
    // ITEMS (KEYS)
    //
    // ================================ //

    public static partial class DGLocalization
    {
        // Weapons
        public static string ITEMS_WEAPONS_HAND => lItems.GetSection("weapons").GetKey("hand");
        public static string ITEMS_WEAPONS_WOODEN_AXE => lItems.GetSection("weapons").GetKey("wooden_axe");
        public static string ITEMS_WEAPONS_STONE_AXE => lItems.GetSection("weapons").GetKey("stone_axe");

        // Materials
        public static string ITEMS_MATERIALS_WOOD => lItems.GetSection("materials").GetKey("wood");
        public static string ITEMS_MATERIALS_STONE => lItems.GetSection("materials").GetKey("stone");

        // Foods
        public static string ITEMS_FOODS_APPLE => lItems.GetSection("foods").GetKey("apple");
        public static string ITEMS_FOODS_BERRY => lItems.GetSection("foods").GetKey("berry");
    }
}