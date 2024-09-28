using DeadlyGame.Core.Enums.Items.Weapons;

namespace DeadlyGame.Core.Items.Templates.Weapons
{
    public abstract class DGWeapon : DGItem
    {
        public DGWeaponType WeaponType { get; set; }
        public int Damage { get; set; }
    }
}
