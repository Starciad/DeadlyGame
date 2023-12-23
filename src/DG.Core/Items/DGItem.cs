using DG.Core.Objects;

namespace DG.Core.Items
{
    public enum DGItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic
    }

    public abstract class DGItem : DGObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DGItemRarity Rarity { get; set; }
        public bool HasDurability { get; set; }
        public int Durability { get; set; }
    }
}
