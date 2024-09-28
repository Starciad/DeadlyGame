using DeadlyGame.Core.Components;
using DeadlyGame.Core.Items.Templates.Weapons;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGEquipmentComponent : DGComponent
    {
        public DGWeapon Weapon { get; private set; }

        // Weapon
        public void EquipWeapon(DGWeapon weapon)
        {
            this.Weapon = weapon;
        }
        public DGWeapon UnequipWeapon()
        {
            DGWeapon target = this.Weapon;
            this.Weapon = null;

            return target;
        }
    }
}