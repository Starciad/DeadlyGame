using DeadlyGame.Core.Behaviors;
using DeadlyGame.Core.Behaviors.Models;
using DeadlyGame.Core.Crafting;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.Items;
using DeadlyGame.Core.Localization;
using DeadlyGame.Core.Models;
using DeadlyGame.Core.Models.Infos.Actions;

namespace DeadlyGame.Core.GameContent.Behaviors
{
    public sealed class DGCraftingBehavior : IDGBehaviour
    {
        // System
        private DGEntity _entity;
        private DGGame _game;

        // Infos
        private DGCraftingRecipe[] _newRecipes;

        // Components
        private DGInventoryComponent _inventoryComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._entity = entity;
            this._game = game;

            if (!entity.ComponentContainer.TryGetComponent(out this._inventoryComponent))
            {
                return false;
            }

            this._newRecipes = game.CraftingDatabase.GetOnlyNewCraftableItems(this._inventoryComponent);

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
            DGCraftingRecipe targetRecipe = this._newRecipes[this._game.RandomMath.Range(0, this._newRecipes.Length)];
            if (targetRecipe.TryCraft(this._inventoryComponent, out DGItem item))
            {
                this._inventoryComponent.AddItem(item, 1);
            }

            // === INFOS ===
            DGPlayerActionInfo infos = new();
            infos.WithName(DGLocalization.MESSAGES_BEHAVIOR_CRAFTING_NAME);
            infos.WithTitle(DGLocalization.GetMessage_Crafting_Title(this._entity.Name));
            infos.WithDescription(DGLocalization.GetMessage_Crafting_Description(this._entity.Name, 1, item.Name));
            infos.WithPriorityLevel(5);
            infos.WithAuthor(this._entity.Id);
            infos.WithColor(DGColor.BurlyWood);
            return infos;
        }
    }
}
