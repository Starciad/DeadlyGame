using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Items
{

    public abstract class DGItem : DGGameObject
    {
        public string Name { get; protected set; }
        public DGItemRarity Rarity { get; protected set; }
        public bool HasDurability { get; protected set; }
        public int Durability { get; protected set; }

        protected DGItem(DGGame game) : base(game)
        {

        }
    }
}
