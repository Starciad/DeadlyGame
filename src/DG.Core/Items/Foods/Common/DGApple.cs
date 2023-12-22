﻿namespace DG.Core.Items.Foods
{
    internal sealed class DGApple : DGFood
    {
        internal override void Build()
        {
            this.Name = "Maçã";
            this.Description = "Deliciosa e apetitosa, deixa sua fome estável.";
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
            this.SatietyFactor = 10;
        }
    }
}