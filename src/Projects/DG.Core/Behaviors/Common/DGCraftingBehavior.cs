using DeadlyGame.Core.Behaviors.Models;
using DeadlyGame.Core.Components.Common;
using DeadlyGame.Core.Crafting;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Information.Actions;
using DeadlyGame.Core.Items;
using DeadlyGame.Core.Localization;

using System.Drawing;

namespace DeadlyGame.Core.Behaviors.Common
{
    internal sealed class DGCraftingBehavior : IDGBehaviour
    {
        // System
        private DGEntity _entity;
        private DGGame _game;

        // Infos
        private DGCraftingRecipe[] _newRecipes;

        // Components
        private DGInventoryComponent _inventoryComponent;

        // Consts
        private const string S_CRAFTING_BEHAVIOR = "Crafting_Behavior";

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
            DGCraftingRecipe targetRecipe = this._newRecipes[this._game.Random.Range(0, this._newRecipes.Length)];
            if (targetRecipe.TryCraft(this._inventoryComponent, out DGItem item))
            {
                this._inventoryComponent.AddItem(item, 1);
            }

            // === INFOS ===
            DGPlayerActionInfo infos = new();
            infos.WithName(DGLocalization.Read(S_CRAFTING_BEHAVIOR, "Name"));
            infos.WithTitle(DGLocalization.Read(S_CRAFTING_BEHAVIOR, "Title"));
            infos.WithDescription(string.Format(DGLocalization.Read(S_CRAFTING_BEHAVIOR, "Description"), this._entity.Name, 1, item.Name));
            infos.WithPriorityLevel(5);
            infos.WithAuthor(this._entity.Id);
            infos.WithColor(Color.BurlyWood);
            return infos;
        }
    }
}
