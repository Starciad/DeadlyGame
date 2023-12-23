using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Crafting;
using DG.Core.Databases;
using DG.Core.Entities;
using DG.Core.Information.Actions;
using DG.Core.Items;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGCraftingBehavior : IDGBehaviour
    {
        private DGCraftingRecipe[] _newRecipes;

        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();

            if (entity.ComponentContainer.TryGetComponent(out DGInventoryComponent inventory))
            {
                this._newRecipes = DGCraftingDatabase.GetOnlyNewCraftableItems(inventory);
                weight.Add(this._newRecipes.Length);
            }

            return weight;
        }

        public DGPlayerActionInfo Act(DGEntity entity, DGGame game)
        {
            DGPlayerActionInfo infos = new();
            if (this._newRecipes.Length == 0)
            {
                infos.WithTitle(string.Empty);
                infos.WithDescription(string.Empty);
                return infos;
            }

            // === ACT ===
            if (entity.ComponentContainer.TryGetComponent(out DGInventoryComponent inventory))
            {
                DGCraftingRecipe targetRecipe = this._newRecipes[game.Random.Range(0, this._newRecipes.Length)];
                if (targetRecipe.TryCraft(inventory, out DGItem item))
                {
                    _ = inventory.TryAddItem(item, 1);
                }
            }

            // === INFOS ===

            infos.WithTitle(string.Empty);
            infos.WithDescription(string.Empty);
            return infos;
        }
    }
}
