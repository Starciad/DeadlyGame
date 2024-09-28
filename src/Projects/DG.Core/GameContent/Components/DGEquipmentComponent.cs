using DeadlyGame.Core.Components;
using DeadlyGame.Core.Items.Templates.Weapons;

namespace DeadlyGame.Core.GameContent.Components
{
    internal sealed class DGEquipmentComponent : DGComponent
    {
        internal DGWeapon Weapon { get; private set; }

        // Weapon
        internal void EquipWeapon(DGWeapon weapon)
        {
            this.Weapon = weapon;
        }
        internal DGWeapon UnequipWeapon()
        {
            DGWeapon target = this.Weapon;
            this.Weapon = null;

            return target;
        }
    }
}