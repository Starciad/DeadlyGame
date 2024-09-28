using DeadlyGame.Core.Enums.Items.Weapons;

namespace DeadlyGame.Core.Items.Types
{
    public abstract class DGWeapon : DGItem
    {
        public DGWeaponType WeaponType { get; set; }
        public int Damage { get; set; }

        protected DGWeapon(DGGame game) : base(game)
        {

        }
    }
}
