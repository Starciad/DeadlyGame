using DG.Core.Items.Attributes;
using DG.Core.Localization;

namespace DG.Core.Items.Materials.Common
{
    [DGItemRegisterAttribute]
    internal sealed class DGWood : DGMaterial
    {
        public DGWood()
        {
            this.Name = DGLocalization.Read("Items", "Material_Wood");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
        }
    }
}
