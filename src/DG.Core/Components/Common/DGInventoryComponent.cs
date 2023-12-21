using DG.Core.Constants;
using DG.Core.Items;

using System;
using System.Collections.Generic;

namespace DG.Core.Components.Common
{
    internal sealed class DGInventoryComponent : DGComponent
    {
        internal int NumberOfSlots => this.numberOfSlots;

        private readonly List<DGInventorySlot> slots = [];
        private int numberOfSlots;

        public override void Initialize()
        {
            this.numberOfSlots = 20;
        }

        public override void Update()
        {
            _ = this.slots.RemoveAll(x => x.IsEmpty);
        }

        internal bool TryAddItem(DGItem item, uint amount)
        {
            if (this.slots.Count < this.numberOfSlots)
            {
                DGInventorySlot targetSlot = this.slots.Find(x => x.Item == item);
                if (targetSlot == null)
                {
                    this.slots.Add(new(item, amount));
                }
                else
                {
                    targetSlot.Add(amount);
                }

                return true;
            }

            return false;
        }

        internal bool TryRemoveItem(DGItem item, uint amount)
        {
            DGInventorySlot targetSlot = this.slots.Find(x => x.Item == item);
            if (targetSlot != null)
            {
                targetSlot.Remove(amount);
                return true;
            }

            return false;
        }

        internal void ModifyNumberOfSlots(uint value)
        {
            this.numberOfSlots = (int)value;
        }
    }

    internal class DGInventorySlot
    {
        internal bool IsEmpty => this.Amount <= 0;
        internal DGItem Item { get; private set; }
        internal int Amount { get; private set; }

        internal DGInventorySlot(DGItem item, uint amount)
        {
            this.Item = item;
            this.Amount = (int)amount;
        }

        internal void Add(uint value)
        {
            this.Amount += (int)value;
            Clamp();
        }

        internal void Remove(uint value)
        {
            this.Amount -= (int)value;
            Clamp();
        }

        private void Clamp()
        {
            this.Amount = Math.Clamp(this.Amount, 0, DGInventoryConstants.MAXIMUM_ITEM_CAPACITY);
        }
    }
}