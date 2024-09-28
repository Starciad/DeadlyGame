using DeadlyGame.Core.Components;
using DeadlyGame.Core.Constants;
using DeadlyGame.Core.Exceptions.Items;
using DeadlyGame.Core.Items;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.Core.GameContent.Components
{
    internal sealed class DGInventoryComponent : DGComponent
    {
        internal DGItem[] Items => this.slots.Select(x => x.Item).ToArray();
        internal DGInventorySlot[] Slots => [.. this.slots];
        internal int NumberOfSlots => this.numberOfSlots;

        private readonly List<DGInventorySlot> slots = [];
        private int numberOfSlots;

        private DGTransformComponent _transformComponent;
        private DGHealthComponent _healthComponent;

        protected override void OnAwake()
        {
            base.OnAwake();

            this.numberOfSlots = 20;

            // TRANSFORM (Component)
            if (this.Entity.ComponentContainer.TryGetComponent(out DGTransformComponent transformComponent))
            {
                this._transformComponent = transformComponent;
            }

            // HEALTH (Component)
            if (this.Entity.ComponentContainer.TryGetComponent(out DGHealthComponent healthComponent))
            {
                this._healthComponent = healthComponent;
                this._healthComponent.OnDied += HealthComponent_OnDied;
            }
        }

        // ===== ADD =====
        internal void AddItem(DGWorldItem worldItem)
        {
            _ = TryAddItem(worldItem);
        }
        internal void AddItem<T>(int amount) where T : DGItem
        {
            AddItem(typeof(T), amount);
        }
        internal void AddItem(DGItem item, int amount)
        {
            _ = TryAddItem(item, amount);
        }
        internal void AddItem(Type itemType, int amount)
        {
            if (!itemType.IsSubclassOf(typeof(DGItem)))
            {
                throw new DGInvalidItemTypeException($"The type representing the item trying to be added to the inventory does not correspond to a valid {nameof(DGItem)}.");
            }

            _ = TryAddItem(this.Game.ItemDatabase.GetItem(itemType), amount);
        }

        // ===== REMOVE =====
        internal void RemoveItem<T>(int amount) where T : DGItem
        {
            RemoveItem(typeof(T), amount);
        }
        internal void RemoveItem(DGItem item, int amount)
        {
            RemoveItem(item.GetType(), amount);
        }
        internal void RemoveItem(Type itemType, int amount)
        {
            _ = TryRemoveItem(itemType, amount);
        }

        // ===== GET =====
        internal DGInventorySlot GetItem<T>() where T : DGItem
        {
            return GetItem(typeof(T));
        }
        internal DGInventorySlot GetItem(DGItem item)
        {
            return GetItem(item.GetType());
        }
        internal DGInventorySlot GetItem(Type itemType)
        {
            _ = TryGetItem(itemType, out DGInventorySlot slot);
            return slot;
        }

        // ===== TRY ADD =====
        internal bool TryAddItem(DGWorldItem worldItem)
        {
            return TryAddItem(worldItem.Item, worldItem.Amount);
        }
        internal bool TryAddItem<T>(int amount) where T : DGItem
        {
            return TryAddItem(typeof(T), amount);
        }
        internal bool TryAddItem(Type itemType, int amount)
        {
            return !itemType.IsSubclassOf(typeof(DGItem))
                ? throw new DGInvalidItemTypeException($"The type representing the item trying to be added to the inventory does not correspond to a valid {nameof(DGItem)}.")
                : TryAddItem(this.Game.ItemDatabase.GetItem(itemType), amount);
        }
        internal bool TryAddItem(DGItem item, int amount)
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
        internal bool TryRemoveItem<T>(int amount) where T : DGItem
        {
            return TryRemoveItem(typeof(T), amount);
        }
        internal bool TryRemoveItem(DGItem item, int amount)
        {
            return TryRemoveItem(item.GetType(), amount);
        }
        internal bool TryRemoveItem(Type itemType, int amount)
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
        internal bool TryGetItem<T>(out DGInventorySlot slot) where T : DGItem
        {
            return TryGetItem(typeof(T), out slot);
        }
        internal bool TryGetItem(DGItem item, out DGInventorySlot slot)
        {
            return TryGetItem(item.GetType(), out slot);
        }
        internal bool TryGetItem(Type itemType, out DGInventorySlot slot)
        {
            slot = this.slots.Find(x => x.ItemType == itemType);
            return slot != null;
        }

        // ===== HAS ITEM =====
        internal bool HasItem<T>() where T : DGItem
        {
            return HasItem(typeof(T));
        }
        internal bool HasItem(DGItem item)
        {
            return HasItem(item.GetType());
        }
        internal bool HasItem(Type itemType)
        {
            return Array.Find(this.Slots, x => x.ItemType == itemType) != null;
        }

        // ===== UTILITIES =====
        internal void ClearInventory()
        {
            this.slots.Clear();
        }
        internal void DropAllItems()
        {
            foreach (DGInventorySlot slot in this.slots)
            {
                this.Game.WorldManager.AddWorldItem(DropItem(slot));
            }

            ClearInventory();
        }
        private DGWorldItem DropItem(DGInventorySlot slot)
        {
            return new DGWorldItem(slot.Item, slot.Amount, this._transformComponent.Position);
        }

        // ===== SLOTS =====
        internal void ModifyNumberOfSlots(int value)
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

    internal class DGInventorySlot(DGItem item, int amount)
    {
        internal bool IsEmpty => this.Amount <= 0;
        internal Type ItemType => this.Item.GetType();
        internal DGItem Item { get; private set; } = item;
        internal int Amount { get; private set; } = amount;

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