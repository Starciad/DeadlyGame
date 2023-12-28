using DG.Core.Items.Attributes;
using DG.Core.Localization;

namespace DG.Core.Items.Foods.Common
{
    [DGItemRegisterAttribute]
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
