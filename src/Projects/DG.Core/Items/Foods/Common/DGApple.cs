using DG.Core.Items.Attributes;
using DG.Core.Localization;

namespace DG.Core.Items.Foods.Common
{
    [DGItemRegisterAttribute]
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
