using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Items.Types;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.GameContent.Items.Foods
{
    public sealed class DGApple : DGFood
    {
        public DGApple(DGGame game) : base(game)
        {
            this.Name = DGLocalization.ITEMS_FOODS_APPLE;
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
            this.SatietyFactor = 10;
        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
        }
    }
}
