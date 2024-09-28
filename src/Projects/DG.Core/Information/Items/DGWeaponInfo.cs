using DeadlyGame.Core.Items.Templates.Weapons;

namespace DeadlyGame.Core.Information.Items
{
    public struct DGWeaponInfo
    {
        public DGItemInfo Info { get; set; }
        public DGWeaponType WeaponType { get; set; }
        public int Damage { get; set; }

        public DGWeaponInfo()
        {
            this.Info = new();
            this.WeaponType = DGWeaponType.None;
            this.Damage = 0;
        }

        internal static DGWeaponInfo Create(DGWeapon weapon)
        {
            return new()
            {
                Info = DGItemInfo.Create(weapon),
                WeaponType = weapon.WeaponType,
                Damage = weapon.Damage,
            };
        }
    }
}
