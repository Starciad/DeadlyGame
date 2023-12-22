namespace DG.Core.Items.Weapons.Common
{
    internal sealed class DGHand : DGWeapon
    {
        internal override void Build()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Rarity = DGItemRarity.Common;
            this.Durability = -1;
            this.WeaponType = DGWeaponType.Melee;
            this.Damage = 2;
        }
    }
}
