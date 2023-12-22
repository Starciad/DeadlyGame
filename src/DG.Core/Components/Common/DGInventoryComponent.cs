using DG.Core.Constants;
using DG.Core.Items;

using System;
using System.Collections.Generic;

namespace DG.Core.Components.Common
{
    internal sealed class DGInventoryComponent : DGComponent
    {
        internal DGInventorySlot[] Slots => this.slots.ToArray();
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

        internal bool TryAddItem(DGItem item, int amount)
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

        internal bool TryRemoveItem(DGItem item, int amount)
        {
            return TryRemoveItem(item.GetType(), amount);
        }

        internal bool TryRemoveItem(Type itemType, int amount)
        {
            DGInventorySlot targetSlot = this.slots.Find(x => x.ItemType == itemType);
            if (targetSlot != null)
            {
                targetSlot.Remove(amount);
                return true;
            }

            return false;
        }

        internal bool HasItem(DGItem item)
        {
            return HasItem(item.GetType());
        }

        internal bool HasItem(Type itemType)
        {
            return Array.Find(Slots, x => x.ItemType == itemType) != null;
        }

        internal void ModifyNumberOfSlots(int value)
        {
            this.numberOfSlots = value;
        }
    }

    internal class DGInventorySlot
    {
        internal bool IsEmpty => this.Amount <= 0;
        internal Type ItemType => this.Item.GetType();
        internal DGItem Item { get; private set; }
        internal int Amount { get; private set; }

        internal DGInventorySlot(DGItem item, int amount)
        {
            this.Item = item;
            this.Amount = amount;
        }

        internal void Add(int value)
        {
            this.Amount += value;
            Clamp();
        }

        internal void Remove(int value)
        {
            this.Amount -= value;
            Clamp();
        }

        private void Clamp()
        {
            this.Amount = Math.Clamp(this.Amount, 0, DGInventoryConstants.MAXIMUM_ITEM_CAPACITY);
        }
    }
}