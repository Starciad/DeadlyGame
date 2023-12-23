using DG.Core.Components.Common;
using DG.Core.Information.Items;
using DG.Core.Items.Accessories;
using DG.Core.Items.Armor;

namespace DG.Core.Information.Components
{
    public struct DGEquipmentInfo
    {
        public DGArmorInfo[] Armor { get; set; }
        public DGAccessoryInfo[] Accessories { get; set; }

        internal static DGEquipmentInfo Create(DGEquipmentComponent component)
        {
            DGArmor[] armor = component.Armor;
            DGAccessory[] accessories = component.Accessories;

            int armorLength = armor.Length;
            int accessoriesLength = accessories.Length;

            DGArmorInfo[] armorInfo = new DGArmorInfo[armorLength];
            DGAccessoryInfo[] accessoriesInfo = new DGAccessoryInfo[accessoriesLength];

            for (int i = 0; i < armorLength; i++)
            {
                armorInfo[i] = DGArmorInfo.Create(armor[i]);
            }

            for (int i = 0; i < accessoriesLength; i++)
            {
                accessoriesInfo[i] = DGAccessoryInfo.Create(accessories[i]);
            }

            return new()
            {
                Armor = armorInfo,
                Accessories = accessoriesInfo,
            };
        }
    }
}