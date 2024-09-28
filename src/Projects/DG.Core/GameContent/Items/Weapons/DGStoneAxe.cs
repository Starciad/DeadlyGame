using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Enums.Items.Weapons;
using DeadlyGame.Core.Items.Templates.Weapons;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.GameContent.Items.Weapons
{
    public sealed class DGStoneAxe : DGWeapon
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
