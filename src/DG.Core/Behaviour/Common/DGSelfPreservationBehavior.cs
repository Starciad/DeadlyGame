using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Effects.Common;
using DG.Core.Entities;
using DG.Core.Information.Actions;
using DG.Core.Items;
using DG.Core.Items.Foods;
using DG.Core.Utilities;

using System;
using System.Collections.Generic;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGSelfPreservationBehavior : IDGBehaviour
    {
        private DGHealthComponent _health;
        private DGHungerComponent _hunger;

        private int healthPercentage;

        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();

            if (entity.ComponentContainer.TryGetComponent(out DGHealthComponent health))
            {
                this._health = health;

                this.healthPercentage = (int)Math.Round(DGPercentageUtilities.CalculatePercentage(this._health.CurrentHealth, this._health.MaximumHealth));
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

            if (entity.ComponentContainer.TryGetComponent(out DGHungerComponent hunger))
            {
                this._hunger = hunger;

                if (this._hunger.IsHungry)
                {
                    weight.Add(10f);
                }
            }

            return weight;
        }

        public DGPlayerActionInfo Act(DGEntity entity, DGGame game)
        {
            DGPlayerActionInfo infos = new();

            // If health is less than 50%, start a rest.
            if (this.healthPercentage <= 50)
            {
                if (entity.ComponentContainer.TryGetComponent(out DGEffectsComponent effectsComponent))
                {
                    effectsComponent.AddEffect<DGRestEffect>();
                }
            }

            // If I'm hungry, I'll eat some food from the inventory.
            if (this._hunger != null && this._hunger.IsHungry)
            {
                if (entity.ComponentContainer.TryGetComponent(out DGInventoryComponent inventoryComponent))
                {
                    List<DGFood> foods = GetFoodFromInventory(inventoryComponent);

                    while (foods.Count > 0 && this._hunger.CurrentHunger > 0)
                    {
                        DGFood randomFood = foods[game.Random.Range(0, foods.Count)];
                        if (!inventoryComponent.HasItem(randomFood))
                        {
                            _ = foods.Remove(randomFood);
                            break;
                        }

                        this._hunger.DecreaseHunger(randomFood.SatietyFactor);
                        inventoryComponent.RemoveItem(randomFood, 1);

                        _ = foods.Remove(randomFood);
                    }
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
