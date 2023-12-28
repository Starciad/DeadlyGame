using DG.Core.Crafting;
using DG.Core.Items.Materials.Common;
using DG.Core.Items.Weapons.Common;

namespace DG.Core.Databases.Crafting
{
    internal partial class DGCraftingDatabase
    {
        private DGCraftingRecipe[] recipes;

        private void InitializeRecipes()
        {
            recipes =
            [
                // Wooden Axe
                new DGCraftingRecipe(this.Game, typeof(DGWoodenAxe), [
                    new(typeof(DGWood), 5)
                ]),

                // Stone Axe
                new DGCraftingRecipe(this.Game, typeof(DGStoneAxe), [
                    new(typeof(DGWood), 6),
                    new(typeof(DGStone), 5)
                ])
            ];
        }
    }
}
