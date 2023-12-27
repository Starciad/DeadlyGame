using DG.Core.Crafting;
using DG.Core.Items.Materials.Common;
using DG.Core.Items.Weapons.Common;

namespace DG.Core.Databases
{
    internal partial class DGCraftingDatabase
    {
        private readonly DGCraftingRecipe[] recipes =
        [
            // Wooden Axe
            new DGCraftingRecipe(typeof(DGWoodenAxe), [
                new(typeof(DGWood), 5)
            ]),

            // Stone Axe
            new DGCraftingRecipe(typeof(DGStoneAxe), [
                new(typeof(DGWood), 6),
                new(typeof(DGStone), 5)
            ])
        ];
    }
}
