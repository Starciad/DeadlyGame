﻿using DG.Core.Constants;
using DG.Core.Exceptions.Items;
using DG.Core.Items;

using System;
using System.Collections.Generic;

namespace DG.Core.Components.Common
{
    public sealed class DGInventoryComponent : DGComponent
    {
        public DGInventorySlot[] Slots => this.slots.ToArray();
        public int NumberOfSlots => this.numberOfSlots;

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
        protected override void OnUpdate()
        {
            _ = this.slots.RemoveAll(x => x.IsEmpty);
        }

        // ===== ADD =====
        public void AddItem<T>(int amount) where T : DGItem
        {
            AddItem(typeof(T), amount);
        }
        public void AddItem(DGItem item, int amount)
        {
            _ = TryAddItem(item, amount);
        }
        public void AddItem(Type itemType, int amount)
        {
            if (!itemType.IsSubclassOf(typeof(DGItem)))
            {
                throw new DGInvalidItemTypeException($"The type representing the item trying to be added to the inventory does not correspond to a valid {nameof(DGItem)}.");
            }

            _ = TryAddItem((DGItem)Activator.CreateInstance(itemType), amount);
        }
        public void AddItem(DGWorldItem worldItem)
        {
            _ = TryAddItem(worldItem);
        }

        // ===== REMOVE =====
        public void RemoveItem<T>(int amount) where T : DGItem
        {
            RemoveItem(typeof(T), amount);
        }
        public void RemoveItem(DGItem item, int amount)
        {
            RemoveItem(item.GetType(), amount);
        }
        public void RemoveItem(Type itemType, int amount)
        {
            _ = TryRemoveItem(itemType, amount);
        }

        // ===== GET =====
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
        public bool TryAddItem<T>(int amount) where T : DGItem
        {
            return TryAddItem(typeof(T), amount);
        }
        public bool TryAddItem(Type itemType, int amount)
        {
            if (!itemType.IsSubclassOf(typeof(DGItem)))
            {
                throw new DGInvalidItemTypeException($"The type representing the item trying to be added to the inventory does not correspond to a valid {nameof(DGItem)}.");
            }

            return TryAddItem((DGItem)Activator.CreateInstance(itemType), amount);
        }
        public bool TryAddItem(DGItem item, int amount)
        {
            if (this.slots.Count < this.numberOfSlots)
            {
                DGInventorySlot targetSlot = this.slots.Find(x => x.ItemType == item.GetType());
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
        public bool TryAddItem(DGWorldItem worldItem)
        {
            return TryAddItem(worldItem.Item, worldItem.Amount);
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
            DGInventorySlot targetSlot = this.slots.Find(x => x.ItemType == itemType);
            if (targetSlot != null)
            {
                targetSlot.Remove(amount);
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
            slot = slots.Find(x => x.ItemType == itemType);
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
            return Array.Find(Slots, x => x.ItemType == itemType) != null;
        }

        // ===== UTILITIES =====
        public void ClearInventory()
        {
            slots.Clear();
            ClearInventory();
        }
        public void DropAllItems()
        {
            foreach (DGInventorySlot slot in this.slots)
            {
                this.Game.WorldManager.AddWorldItem(DropItem(slot));
            }
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