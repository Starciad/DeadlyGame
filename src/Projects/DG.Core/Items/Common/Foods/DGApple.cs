using DeadlyGame.Core.Items.Templates.Foods;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.Items.Common.Foods
{
    internal sealed class DGApple : DGFood
    {
        public DGApple()
        {
            this.Name = DGLocalization.Read("Items", "Food_Apple");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
            this.SatietyFactor = 10;
        }
    }
}
