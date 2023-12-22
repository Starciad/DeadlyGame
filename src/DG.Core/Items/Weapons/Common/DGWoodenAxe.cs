namespace DG.Core.Items.Weapons.Common
{
    internal sealed class DGWoodenAxe : DGWeapon
    {
        public override void Build()
        {
            this.Name = "Machado de Madeira";
            this.Description = "Pode ser usado para coletar madeira e como uma arma.";
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = true;
            this.Durability = 20;
            this.WeaponType = DGWeaponType.Melee;
            this.Damage = 4;
        }
    }
}
