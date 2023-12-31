﻿using DG.Core.Items.Attributes;
using DG.Core.Localization;

namespace DG.Core.Items.Materials.Common
{
    [DGItemRegisterAttribute]
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
