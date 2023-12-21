using System.ComponentModel.DataAnnotations;

namespace DG.Core.Items.Armor
{
    internal enum DGArmorType
    {
        Helmet = 0,
        Breastplate = 1,
        Pants = 2,
        Boots = 3,
        Shield = 4
    }

    internal abstract class DGArmor : DGItem
    {
        [Range(0, 5)] internal float Defense { get; set; }
        internal DGArmorType ArmorType { get; set; }
    }
}
