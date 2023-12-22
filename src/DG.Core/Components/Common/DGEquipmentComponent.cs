using DG.Core.Constants;
using DG.Core.Items.Accessories;
using DG.Core.Items.Armor;
using DG.Core.Items.Weapons;

using System.Collections.Generic;

namespace DG.Core.Components.Common
{
    internal sealed class DGEquipmentComponent : DGComponent
    {
        internal DGArmor[] Armor { get; } = new DGArmor[DGInventoryConstants.MAXIMUM_ARMOR_CAPACITY];
        internal DGAccessory[] Accessories => this.accessories.ToArray();
        internal DGWeapon Weapon { get; private set; }

        private readonly List<DGAccessory> accessories = new(DGInventoryConstants.MAXIMUM_ACCESSORY_CAPACITY);

        // Armor
        internal float GetArmoredClass()
        {
            float value = 0;

            foreach (DGArmor armor in this.Armor)
            {
                if (armor == null)
                {
                    continue;
                }

                value += armor.Defense;
            }

            return value;
        }
        internal DGArmor GetArmor(DGArmorType armorType)
        {
            return this.Armor[(int)armorType];
        }
        internal DGArmor UnequipArmor(DGArmorType armorType)
        {
            int slot = (int)armorType;
            DGArmor targetArmor = this.Armor[slot];
            this.Armor[slot] = null;

            return targetArmor;
        }
        internal void EquipArmor(DGArmor newArmor, out DGArmor oldArmor)
        {
            oldArmor = UnequipArmor(newArmor.ArmorType);
            this.Armor[(int)newArmor.ArmorType] = newArmor;
        }

        // Accessories
        internal bool TryEquipAccessory(DGAccessory accessory)
        {
            if (this.accessories.Count < DGInventoryConstants.MAXIMUM_ACCESSORY_CAPACITY)
            {
                this.accessories.Add(accessory);
                return true;
            }

            return false;
        }
        internal bool TryUnequipAccessory(DGAccessory accessory)
        {
            return this.accessories.Remove(accessory);
        }

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