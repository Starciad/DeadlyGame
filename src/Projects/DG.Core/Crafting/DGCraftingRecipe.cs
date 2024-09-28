using DG.Core.Components.Common;
using DG.Core.Exceptions.Items;
using DG.Core.Items;

using System;

namespace DG.Core.Crafting
{
    internal sealed class DGCraftingRecipe
    {
        internal Type ItemType { get; private set; }
        internal DGCraftingMaterial[] RequiredMaterials { get; private set; }

        private readonly DGGame _game;

        internal DGCraftingRecipe(DGGame game, Type itemType, DGCraftingMaterial[] materials)
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

        internal bool CanCraft(DGInventoryComponent inventoryComponent)
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

        internal bool TryCraft(DGInventoryComponent inventoryComponent, out DGItem item)
        {
            item = default;

            if (!CanCraft(inventoryComponent))
            {
                return false;
            }

            foreach (DGCraftingMaterial requiredMaterial in this.RequiredMaterials)
            {
                _ = inventoryComponent.TryRemoveItem(requiredMaterial.ItemType, requiredMaterial.Count);
            }

            DGItem targetItem = this._game.ItemDatabase.GetItem(this.ItemType);
            item = targetItem;
            return true;
        }
    }

    internal struct DGCraftingMaterial
    {
        internal Type ItemType { get; private set; }
        internal int Count { get; private set; }

        internal DGCraftingMaterial(Type itemType, int count)
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
