using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Items.Templates.Materials;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.Items.Common.Materials
{
    internal sealed class DGStone : DGMaterial
    {
        public DGStone()
        {
            this.Name = DGLocalization.Read("Items", "Material_Stone");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
        }
    }
}
