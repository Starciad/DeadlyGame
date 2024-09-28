using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Items.Templates.Foods;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.Items.Common.Foods
{
    internal sealed class DGBerry : DGFood
    {
        public DGBerry()
        {
            this.Name = DGLocalization.Read("Items", "Food_Berry");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
            this.SatietyFactor = 5;
        }
    }
}
