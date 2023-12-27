using DG.Core.Behaviors;
using DG.Core.Behaviors.Models;
using DG.Core.Components.Common;
using DG.Core.Effects.Common;
using DG.Core.Entities;
using DG.Core.Information.Actions;
using DG.Core.Items;
using DG.Core.Items.Foods;
using DG.Core.Utilities;

using System;
using System.Collections.Generic;

namespace DG.Core.Behaviors.Common
{
    internal sealed class DGSelfPreservationBehavior : IDGBehaviour
    {
        // System
        private DGEntity _entity;
        private DGGame _game;

        // Infos
        private int healthPercentage;

        // Components
        private DGHealthComponent _healthComponent;
        private DGHungerComponent _hungerComponent;
        private DGEffectsComponent _effectsComponent;
        private DGInventoryComponent _inventoryComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._entity = entity;
            this._game = game;

            this._healthComponent = entity.ComponentContainer.GetComponent<DGHealthComponent>();
            this._hungerComponent = entity.ComponentContainer.GetComponent<DGHungerComponent>();
            this._effectsComponent = entity.ComponentContainer.GetComponent<DGEffectsComponent>();
            this._inventoryComponent = entity.ComponentContainer.GetComponent<DGInventoryComponent>();

            return true;
        }
        public DGBehaviourWeight GetWeight()
        {
            DGBehaviourWeight weight = new();

            // HEALTH
            if (this._healthComponent != null)
            {
                this.healthPercentage = (int)Math.Round(DGPercentageUtilities.CalculatePercentage(this._healthComponent.CurrentHealth, this._healthComponent.MaximumHealth));
                if (this.healthPercentage <= 50)
                {
                    weight.Add(5f);
                }

                if (this.healthPercentage <= 25)
                {
                    weight.Add(5f);
                }

                if (this.healthPercentage <= 10)
                {
                    weight.Add(10f);
                }
            }

            // HUNGER
            if (this._hungerComponent != null && this._hungerComponent.IsHungry)
            {
                weight.Add(10f);
            }

            return weight;
        }
        public DGPlayerActionInfo Act()
        {
            DGPlayerActionInfo infos = new();

            // If health is less than 50%, start a rest.
            if (this._effectsComponent != null)
            {
                if (this.healthPercentage <= 50)
                {
                    this._effectsComponent.AddEffect<DGRestEffect>();
                }
            }

            // If I'm hungry, I'll eat some food from the inventory.
            if (this._hungerComponent != null &&
                this._inventoryComponent != null)
            {
                List<DGFood> foods = GetFoodFromInventory(this._inventoryComponent);

                while (foods.Count > 0 && this._hungerComponent.CurrentHunger > 0)
                {
                    DGFood randomFood = foods[this._game.Random.Range(0, foods.Count)];
                    if (!this._inventoryComponent.HasItem(randomFood))
                    {
                        _ = foods.Remove(randomFood);
                        break;
                    }

                    this._hungerComponent.DecreaseHunger(randomFood.SatietyFactor);
                    this._inventoryComponent.RemoveItem(randomFood, 1);

                    _ = foods.Remove(randomFood);
                }
            }

            return infos;
        }

        private static List<DGFood> GetFoodFromInventory(DGInventoryComponent inventoryComponent)
        {
            List<DGFood> foodsFound = [];

            foreach (DGItem item in inventoryComponent.Items)
            {
                if (item.GetType().IsSubclassOf(typeof(DGFood)))
                {
                    foodsFound.Add((DGFood)item);
                }
            }

            return foodsFound;
        }
    }
}
