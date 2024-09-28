using DeadlyGame.Core.Crafting;
using DeadlyGame.Core.GameContent.Items.Materials;
using DeadlyGame.Core.GameContent.Items.Weapons;

namespace DeadlyGame.Core.Databases.Crafting
{
    public partial class DGCraftingDatabase
    {
        private DGCraftingRecipe[] recipes;

        private void InitializeRecipes()
        {
            this.recipes =
            [
                // Wooden Axe
                new DGCraftingRecipe(this.DGGameInstance, typeof(DGWoodenAxe), [
                    new(typeof(DGWood), 5)
                ]),

                // Stone Axe
                new DGCraftingRecipe(this.DGGameInstance, typeof(DGStoneAxe), [
                    new(typeof(DGWood), 6),
                    new(typeof(DGStone), 5)
                ])
            ];
        }
    }
}
