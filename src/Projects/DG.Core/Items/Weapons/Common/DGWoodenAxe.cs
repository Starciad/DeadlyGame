using DG.Core.Items.Attributes;
using DG.Core.Localization;

namespace DG.Core.Items.Weapons.Common
{
    [DGItemRegisterAttribute]
    internal sealed class DGWoodenAxe : DGWeapon
    {
        public DGWoodenAxe()
        {
            this.Name = DGLocalization.Read("Items", "Weapon_WoodenAxe");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = true;
            this.Durability = 20;
            this.WeaponType = DGWeaponType.Melee;
            this.Damage = 4;
        }
    }
}
