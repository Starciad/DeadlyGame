namespace DG.Core.Items.Weapons.Common
{
    internal sealed class DGStoneAxe : DGWeapon
    {
        public DGStoneAxe()
        {
            this.Name = "Machado de Pedra";
            this.Description = "Pode ser usado para coletar madeira e como uma arma.";
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = true;
            this.Durability = 30;
            this.WeaponType = DGWeaponType.Melee;
            this.Damage = 5;
        }
    }
}
