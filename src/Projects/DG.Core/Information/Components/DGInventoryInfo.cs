using DeadlyGame.Core.Components.Common;
using DeadlyGame.Core.Information.Items;

namespace DeadlyGame.Core.Information.Components
{
    public struct DGInventoryInfo
    {
        public DGInventorySlotInfo[] Slots { get; set; }

        public DGInventoryInfo()
        {
            this.Slots = [];
        }

        internal static DGInventoryInfo Create(DGInventoryComponent component)
        {
            DGInventorySlot[] inventorySlots = component.Slots;
            int inventorySlotsLength = inventorySlots.Length;

            DGInventorySlotInfo[] slotsInfo = new DGInventorySlotInfo[inventorySlotsLength];
            for (int i = 0; i < inventorySlotsLength; i++)
            {
                slotsInfo[i] = DGInventorySlotInfo.Create(inventorySlots[i]);
            }

            return new()
            {
                Slots = slotsInfo
            };
        }
    }

    public struct DGInventorySlotInfo
    {
        public DGItemInfo Item { get; set; }
        public int Amount { get; set; }

        public DGInventorySlotInfo()
        {
            this.Item = new();
            this.Amount = 0;
        }

        internal static DGInventorySlotInfo Create(DGInventorySlot slot)
        {
            return new()
            {
                Item = DGItemInfo.Create(slot.Item),
                Amount = slot.Amount
            };
        }
    }
}
