using DG.Core.Items.Attributes;
using DG.Core.Localization;

namespace DG.Core.Items.Weapons.Common
{
    [DGItemRegisterAttribute]
    internal sealed class DGStoneAxe : DGWeapon
    {
        public DGStoneAxe()
        {
            this.Name = DGLocalization.Read("Items", "Weapon_StoneAxe");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = true;
            this.Durability = 30;
            this.WeaponType = DGWeaponType.Melee;
            this.Damage = 5;
        }
    }
}
