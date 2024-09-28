using DeadlyGame.Core.Components;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Items.Types;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGEquipmentComponent : DGComponent
    {
        public DGWeapon Weapon { get; private set; }

        public DGEquipmentComponent(DGGame game, DGEntity entity) : base(game, entity)
        {

        }

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