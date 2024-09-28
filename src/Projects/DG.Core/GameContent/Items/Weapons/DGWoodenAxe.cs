using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Enums.Items.Weapons;
using DeadlyGame.Core.Items.Templates.Weapons;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.GameContent.Items.Weapons
{
    public sealed class DGWoodenAxe : DGWeapon
    {
        public DGWoodenAxe(DGGame game) : base(game)
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
