using DG.Core.Components.Common;
using DG.Core.Information.Items;

namespace DG.Core.Information.Components
{
    public struct DGEquipmentInfo
    {
        public DGWeaponInfo Weapon { get; set; }

        public DGEquipmentInfo()
        {
            this.Weapon = new();
        }

        internal static DGEquipmentInfo Create(DGEquipmentComponent component)
        {
            // WEAPON
            DGWeaponInfo weaponInfo = new();
            if (component.Weapon != null)
            {
                weaponInfo = DGWeaponInfo.Create(component.Weapon);
            }

            return new()
            {
                Weapon = weaponInfo,
            };
        }
    }
}