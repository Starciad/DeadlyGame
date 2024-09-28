using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Items
{

    internal abstract class DGItem : DGObject
    {
        internal string Name { get; set; }
        internal DGItemRarity Rarity { get; set; }
        internal bool HasDurability { get; set; }
        internal int Durability { get; set; }
    }
}
