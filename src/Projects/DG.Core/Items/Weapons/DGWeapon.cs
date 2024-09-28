namespace DG.Core.Items.Weapons
{
    public enum DGWeaponType
    {
        None = -1,
        Melee = 0,
        Ranged = 1,
        Magic = 2
    }

    internal abstract class DGWeapon : DGItem
    {
        internal DGWeaponType WeaponType { get; set; }
        internal int Damage { get; set; }
    }
}
