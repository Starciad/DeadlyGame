using DG.Core.Constants;
using DG.Core.Items.Accessories;
using DG.Core.Items.Armor;

using System.Collections.Generic;

namespace DG.Core.Components.Common
{
    internal sealed class DGEquipmentComponent : DGComponent
    {
        internal DGArmor[] Armor { get; } = new DGArmor[DGInventoryConstants.MAXIMUM_ARMOR_CAPACITY];
        internal DGAccessory[] Accessories => accessories.ToArray();

        private readonly List<DGAccessory> accessories = new(DGInventoryConstants.MAXIMUM_ACCESSORY_CAPACITY);

        // Armor
        internal float GetArmoredClass()
        {
            float value = 0;

            foreach (DGArmor armor in Armor)
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
            return Armor[(int)armorType];
        }
        internal DGArmor UnequipArmor(DGArmorType armorType)
        {
            int slot = (int)armorType;
            DGArmor targetArmor = Armor[slot];
            Armor[slot] = null;

            return targetArmor;
        }
        internal void EquipArmor(DGArmor newArmor, out DGArmor oldArmor)
        {
            oldArmor = UnequipArmor(newArmor.ArmorType);
            Armor[(int)newArmor.ArmorType] = newArmor;
        }

        // Accessories
        internal bool TryEquipAccessory(DGAccessory accessory)
        {
            if (accessories.Count < DGInventoryConstants.MAXIMUM_ACCESSORY_CAPACITY)
            {
                accessories.Add(accessory);
                return true;
            }

            return false;
        }
        internal bool TryUnequipAccessory(DGAccessory accessory)
        {
            return accessories.Remove(accessory);
        }
    }
}