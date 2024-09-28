using DeadlyGame.Core.Components;
using DeadlyGame.Core.Constants;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Items;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGInventoryComponent : DGComponent
    {
        public DGItem[] Items => this.slots.Select(x => x.Item).ToArray();
        public DGInventorySlot[] Slots => [.. this.slots];
        public int NumberOfSlots => this.numberOfSlots;

        private readonly List<DGInventorySlot> slots = [];
        private int numberOfSlots;

        private readonly DGTransformComponent _transformComponent;
        private readonly DGHealthComponent _healthComponent;

        public DGInventoryComponent(DGGame game, DGEntity entity) : base(game, entity)
        {
            this.numberOfSlots = 20;

            // TRANSFORM (Component)
            if (entity.ComponentContainer.TryGetComponent(out DGTransformComponent transformComponent))
            {
                this._transformComponent = transformComponent;
            }

            // HEALTH (Component)
            if (entity.ComponentContainer.TryGetComponent(out DGHealthComponent healthComponent))
            {
                this._healthComponent = healthComponent;
                this._healthComponent.OnDied += HealthComponent_OnDied;
            }
        }

        public void AddItem(DGItem item, int amount)
        {
            _ = TryAddItem(item, amount);
        }

        public void RemoveItem(DGItem item, int amount)
        {
            RemoveItem(item, amount);
        }

        public DGInventorySlot GetItem<T>() where T : DGItem
        {
            return GetItem(typeof(T));
        }
        public DGInventorySlot GetItem(DGItem item)
        {
            return GetItem(item.GetType());
        }
        public DGInventorySlot GetItem(Type itemType)
        {
            _ = TryGetItem(itemType, out DGInventorySlot slot);
            return slot;
        }

        // ===== TRY ADD =====
        public bool TryAddItem(DGItem item, int amount)
        {
            // If the corresponding item is already in the inventory, just increase its count and return true.
            if (TryGetItem(item.GetType(), out DGInventorySlot slot))
            {
                // If the item has not extended its limit, increment the count value, but if it has, add a new slot that will contain a new stack of the same item.
                if (slot.Amount < DGInventoryConstants.MAXIMUM_ITEM_CAPACITY)
                {
                    slot.Add(amount);
                    return true;
                }
            }

            // If the new item is not registered in the inventory, check if there is space and, if so, add the new slot with the item information.
            if (this.slots.Count < this.numberOfSlots)
            {
                this.slots.Add(new(item, amount));
                return true;
            }

            // If there is not enough space in the inventory, return false.
            return false;
        }

        // ===== TRY REMOVE =====
        public bool TryRemoveItem<T>(int amount) where T : DGItem
        {
            return TryRemoveItem(typeof(T), amount);
        }
        public bool TryRemoveItem(DGItem item, int amount)
        {
            return TryRemoveItem(item.GetType(), amount);
        }
        public bool TryRemoveItem(Type itemType, int amount)
        {
            if (TryGetItem(itemType, out DGInventorySlot slot))
            {
                slot.Remove(amount);

                if (slot.IsEmpty)
                {
                    _ = this.slots.Remove(slot);
                }

                return true;
            }

            return false;
        }

        // ===== TRY GET =====
        public bool TryGetItem<T>(out DGInventorySlot slot) where T : DGItem
        {
            return TryGetItem(typeof(T), out slot);
        }
        public bool TryGetItem(DGItem item, out DGInventorySlot slot)
        {
            return TryGetItem(item.GetType(), out slot);
        }
        public bool TryGetItem(Type itemType, out DGInventorySlot slot)
        {
            slot = this.slots.Find(x => x.ItemType == itemType);
            return slot != null;
        }

        // ===== HAS ITEM =====
        public bool HasItem<T>() where T : DGItem
        {
            return HasItem(typeof(T));
        }
        public bool HasItem(DGItem item)
        {
            return HasItem(item.GetType());
        }
        public bool HasItem(Type itemType)
        {
            return Array.Find(this.Slots, x => x.ItemType == itemType) != null;
        }

        // ===== UTILITIES =====
        public void ClearInventory()
        {
            this.slots.Clear();
        }
        public void DropAllItems()
        {
            foreach (DGInventorySlot slot in this.slots)
            {
                this.DGGameInstance.WorldManager.AddWorldItem(DropItem(slot));
            }

            ClearInventory();
        }
        private DGWorldItem DropItem(DGInventorySlot slot)
        {
            return new DGWorldItem(slot.Item, slot.Amount, this._transformComponent.Position);
        }

        // ===== SLOTS =====
        public void ModifyNumberOfSlots(int value)
        {
            this.numberOfSlots = value;
        }

        // ===== EVENTS =====
        private void HealthComponent_OnDied()
        {
            this._healthComponent.OnDied -= HealthComponent_OnDied;
            DropAllItems();
        }
    }

    public class DGInventorySlot(DGItem item, int amount)
    {
        public bool IsEmpty => this.Amount <= 0;
        public Type ItemType => this.Item.GetType();
        public DGItem Item { get; private set; } = item;
        public int Amount { get; private set; } = amount;

        public void Add(int value)
        {
            this.Amount += value;
            Clamp();
        }
        public void Remove(int value)
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