using DG.Core.Objects;

namespace DG.Core.Items
{
    internal enum DGItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic
    }

    internal abstract class DGItem : DGObject
    {
        internal string Name { get; set; }
        internal string Description { get; set; }
        internal DGItemRarity Rarity { get; set; }
        internal bool HasDurability { get; set; }
        internal int Durability { get; set; }

        internal abstract void Build();
    }
}
