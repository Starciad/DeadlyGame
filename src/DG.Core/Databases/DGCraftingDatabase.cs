using DG.Core.Components.Common;
using DG.Core.Crafting;

using System.Collections.Generic;
using System.Linq;

namespace DG.Core.Databases
{
    internal static partial class DGCraftingDatabase
    {
        internal static DGCraftingRecipe[] GetOnlyNewCraftableItems(DGInventoryComponent inventoryComponent)
        {
            return [.. GetCraftableItems(inventoryComponent).Where(x => !inventoryComponent.HasItem(x.ItemType))];
        }

        internal static DGCraftingRecipe[] GetCraftableItems(DGInventoryComponent inventoryComponent)
        {
            List<DGCraftingRecipe> selectedRecipes = [];

            foreach (DGCraftingRecipe recipe in recipes)
            {
                if (recipe.CanCraft(inventoryComponent))
                {
                    selectedRecipes.Add(recipe);
                }
            }

            return [.. selectedRecipes];
        }
    }
}
