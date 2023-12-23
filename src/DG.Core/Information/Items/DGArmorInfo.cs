using DG.Core.Items.Armor;

namespace DG.Core.Information.Items
{
    public struct DGArmorInfo
    {
        public DGItemInfo Info { get; set; }
        public float Defense { get; set; }
        public DGArmorType ArmorType { get; set; }

        internal static DGArmorInfo Create(DGArmor armor)
        {
            return new()
            {
                Info = DGItemInfo.Create(armor),
                Defense = armor.Defense,
                ArmorType = armor.ArmorType,
            };
        }
    }
}
