using DG.Core.Objects;

namespace DG.Core.Items
{
    public enum DGItemRarity
    {
        None = -1,
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Epic = 3,
        Legendary = 4,
        Mythic = 5
    }

    internal abstract class DGItem : DGObject
    {
        internal string Name { get; set; }
        internal DGItemRarity Rarity { get; set; }
        internal bool HasDurability { get; set; }
        internal int Durability { get; set; }
    }
}
