using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Items.Templates.Materials;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.GameContent.Items.Materials
{
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
