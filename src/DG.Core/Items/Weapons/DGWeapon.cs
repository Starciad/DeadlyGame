namespace DG.Core.Items.Weapons
{
    internal enum DGWeaponType
    {
        Melee,
        Ranged,
        Magic
    }

    internal abstract class DGWeapon : DGItem
    {
        internal DGWeaponType WeaponType { get; set; }
        internal int Damage { get; set; }
    }
}
