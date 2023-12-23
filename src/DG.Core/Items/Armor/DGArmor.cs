using System;
using System.ComponentModel.DataAnnotations;

namespace DG.Core.Items.Armor
{
    public enum DGArmorType
    {
        Helmet = 0,
        Breastplate = 1,
        Pants = 2,
        Boots = 3,
        Shield = 4
    }

    internal abstract class DGArmor : DGItem
    {
        internal float Defense { get; private set; }
        internal DGArmorType ArmorType { get; set; }

        internal void SetDefense(int value)
        {
            this.Defense = value;
            this.Defense = (float)Math.Clamp(this.Defense, 0f, 5f);
        }
    }
}
