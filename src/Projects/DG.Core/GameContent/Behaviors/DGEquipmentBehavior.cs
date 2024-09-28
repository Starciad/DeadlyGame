using DeadlyGame.Core;
using DeadlyGame.Core.Behaviors;
using DeadlyGame.Core.Behaviors.Models;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.Items.Templates.Weapons;
using DeadlyGame.Core.Models.Infos.Actions;

using System.Linq;

namespace DeadlyGame.Core.GameContent.Behaviors
{
    internal sealed class DGEquipmentBehavior : IDGBehaviour
    {
        private DGInventoryComponent inventoryComponent;
        private DGEquipmentComponent equipmentComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            _ = entity.ComponentContainer.TryGetComponent(out this.inventoryComponent);
            _ = entity.ComponentContainer.TryGetComponent(out this.equipmentComponent);

            // Always equip the strongest weapon in your inventory.
            DGWeapon currentWeapon = this.equipmentComponent.Weapon;
            DGWeapon bestWeapon = currentWeapon;

            DGWeapon[] weapons = this.inventoryComponent.Items.Where(x => x.GetType().IsSubclassOf(typeof(DGWeapon))).Cast<DGWeapon>().ToArray();
            if (currentWeapon == null && weapons.Length > 0)
            {
                this.equipmentComponent.EquipWeapon(weapons[0]);
                return false;
            }

            foreach (DGWeapon weapon in weapons)
            {
                if (weapon.Damage > currentWeapon.Damage &&
                    weapon.Damage > bestWeapon.Damage)
                {
                    bestWeapon = weapon;
                }
            }

            this.equipmentComponent.EquipWeapon(bestWeapon);
            return false;
        }

        public DGBehaviourWeight GetWeight()
        {
            return default;
        }

        public DGPlayerActionInfo Act()
        {
            return default;
        }
    }
}
