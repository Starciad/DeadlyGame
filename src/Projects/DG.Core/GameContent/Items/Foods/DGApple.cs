using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Items.Templates.Foods;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.GameContent.Items.Foods
{
    public sealed class DGApple : DGFood
    {
        public DGApple(DGGame game) : base(game)
        {
            this.Name = DGLocalization.Read("Items", "Food_Apple");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
            this.SatietyFactor = 10;
        }
    }
}
