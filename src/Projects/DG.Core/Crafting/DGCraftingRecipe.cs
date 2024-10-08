﻿using DeadlyGame.Core.Exceptions.Items;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.Items;

using System;

namespace DeadlyGame.Core.Crafting
{
    public sealed class DGCraftingRecipe
    {
        public Type ItemType { get; private set; }
        public DGCraftingMaterial[] RequiredMaterials { get; private set; }

        private readonly DGGame _game;

        public DGCraftingRecipe(DGGame game, Type itemType, DGCraftingMaterial[] materials)
        {
            if (!itemType.IsSubclassOf(typeof(DGItem)))
            {
                throw new DGInvalidItemTypeException($"The type defined in {nameof(DGCraftingRecipe)} is not a {nameof(DGItem)}.");
            }

            if (materials.Length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(materials), "A recipe must have at least one required material.");
            }

            this._game = game;
            this.ItemType = itemType;
            this.RequiredMaterials = materials;
        }

        public bool CanCraft(DGInventoryComponent inventoryComponent)
        {
            foreach (DGCraftingMaterial requiredMaterial in this.RequiredMaterials)
            {
                DGInventorySlot slot = Array.Find(inventoryComponent.Slots, x => x.ItemType == requiredMaterial.ItemType);

                if (slot == null)
                {
                    return false;
                }

                if (slot.Amount < requiredMaterial.Count)
                {
                    return false;
                }
            }

            return true;
        }

        public bool TryCraft(DGInventoryComponent inventoryComponent, out DGItem item)
        {
            item = null;

            if (!CanCraft(inventoryComponent))
            {
                return false;
            }

            foreach (DGCraftingMaterial requiredMaterial in this.RequiredMaterials)
            {
                _ = inventoryComponent.TryRemoveItem(requiredMaterial.ItemType, requiredMaterial.Count);
            }

            item = (DGItem)Activator.CreateInstance(this.ItemType, [this._game]);

            return true;
        }
    }

    public struct DGCraftingMaterial
    {
        public Type ItemType { get; private set; }
        public int Count { get; private set; }

        public DGCraftingMaterial(Type itemType, int count)
        {
            if (!itemType.IsSubclassOf(typeof(DGItem)))
            {
                throw new DGInvalidItemTypeException("The type defined in DGCraftingMaterial is not a DGItem.");
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "The count value must be greater than zero.");
            }

            this.ItemType = itemType;
            this.Count = count;
        }
    }
}
