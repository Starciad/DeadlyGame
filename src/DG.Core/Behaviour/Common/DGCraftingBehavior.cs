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
        // System
        private DGGame _game;

        // Infos
        private DGCraftingRecipe[] _newRecipes;

        // Components
        private DGInventoryComponent _inventoryComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._game = game;

            if (!entity.ComponentContainer.TryGetComponent(out this._inventoryComponent))
            {
                return false;
            }

            this._newRecipes = DGCraftingDatabase.GetOnlyNewCraftableItems(this._inventoryComponent);

            return this._newRecipes != null && this._newRecipes.Length != 0;
        }
        public DGBehaviourWeight GetWeight()
        {
            DGBehaviourWeight weight = new();
            weight.Add(this._newRecipes.Length);
            return weight;
        }
        public DGPlayerActionInfo Act()
        {
            // === ACT ===
            DGCraftingRecipe targetRecipe = this._newRecipes[this._game.Random.Range(0, this._newRecipes.Length)];
            if (targetRecipe.TryCraft(this._inventoryComponent, out DGItem item))
            {
                this._inventoryComponent.AddItem(item, 1);
            }

            // === INFOS ===
            DGPlayerActionInfo infos = new();
            infos.WithTitle(string.Empty);
            infos.WithDescription(string.Empty);
            return infos;
        }
    }
}
