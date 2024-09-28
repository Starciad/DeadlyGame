using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Items.Templates.Foods;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.GameContent.Items.Foods
{
    public sealed class DGBerry : DGFood
    {
        public DGBerry(DGGame game) : base(game)
        {
            this.Name = DGLocalization.Read("Items", "Food_Berry");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
            this.SatietyFactor = 5;
        }
    }
}
