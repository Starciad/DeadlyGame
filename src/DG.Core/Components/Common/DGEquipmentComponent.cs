using DG.Core.Constants;
using DG.Core.Items.Accessories;
using DG.Core.Items.Armor;

namespace DG.Core.Components.Common
{
    internal sealed class DGEquipmentComponent
    {
        // Armor Parts
        internal DGHelmet Helmet { get; private set; }
        internal DGBreastplate Breastplate { get; private set; }
        internal DGPants Pants { get; private set; }
        internal DGBoots Boots { get; private set; }

        // Accessories
        internal DGAccessory[] Accessories { get; private set; } = new DGAccessory[DGInventoryConstants.MAXIMUM_ACCESSORY_CAPACITY];

        internal int GetArmoredClass()
        {
            return Helmet.Defense + Breastplate.Defense + Pants.Defense + Boots.Defense;
        }
    }
}