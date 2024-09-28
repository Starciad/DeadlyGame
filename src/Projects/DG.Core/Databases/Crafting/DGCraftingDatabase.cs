using DeadlyGame.Core.Crafting;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.Objects;

using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.Core.Databases.Crafting
{
    public partial class DGCraftingDatabase : DGObject
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            InitializeRecipes();
        }

        public DGCraftingRecipe[] GetOnlyNewCraftableItems(DGInventoryComponent inventoryComponent)
        {
            return [.. GetCraftableItems(inventoryComponent).Where(x => !inventoryComponent.HasItem(x.ItemType))];
        }

        public DGCraftingRecipe[] GetCraftableItems(DGInventoryComponent inventoryComponent)
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
