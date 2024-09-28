using System.Collections.Generic;
using System.Linq;

namespace DG.Core.Databases.Crafting
{
    internal partial class DGCraftingDatabase : DGObject
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            InitializeRecipes();
        }

        internal DGCraftingRecipe[] GetOnlyNewCraftableItems(DGInventoryComponent inventoryComponent)
        {
            return [.. GetCraftableItems(inventoryComponent).Where(x => !inventoryComponent.HasItem(x.ItemType))];
        }

        internal DGCraftingRecipe[] GetCraftableItems(DGInventoryComponent inventoryComponent)
        {
            List<DGCraftingRecipe> selectedRecipes = [];

            foreach (DGCraftingRecipe recipe in this.recipes)
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
