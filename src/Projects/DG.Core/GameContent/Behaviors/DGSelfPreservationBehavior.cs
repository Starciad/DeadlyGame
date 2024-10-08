﻿using DeadlyGame.Core.Behaviors;
using DeadlyGame.Core.Behaviors.Models;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Effects;
using DeadlyGame.Core.Items;
using DeadlyGame.Core.Items.Types;
using DeadlyGame.Core.Localization;
using DeadlyGame.Core.Mathematics;
using DeadlyGame.Core.Models;
using DeadlyGame.Core.Models.Infos.Actions;

using System;
using System.Collections.Generic;
using System.Text;

namespace DeadlyGame.Core.GameContent.Behaviors
{
    public sealed class DGSelfPreservationBehavior : IDGBehaviour
    {
        // System
        private DGEntity _entity;
        private DGGame _game;

        // Infos
        private int healthPercentage;
        private readonly StringBuilder descriptionStringBuilder = new();

        // Components
        private DGHealthComponent _healthComponent;
        private DGHungerComponent _hungerComponent;
        private DGEffectsComponent _effectsComponent;
        private DGInventoryComponent _inventoryComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            bool result = false;

            this._entity = entity;
            this._game = game;

            this._healthComponent = entity.ComponentContainer.GetComponent<DGHealthComponent>();
            this._hungerComponent = entity.ComponentContainer.GetComponent<DGHungerComponent>();
            this._effectsComponent = entity.ComponentContainer.GetComponent<DGEffectsComponent>();
            this._inventoryComponent = entity.ComponentContainer.GetComponent<DGInventoryComponent>();

            if (this._healthComponent != null)
            {
                this.healthPercentage = (int)Math.Round(DGPercentageMath.GetPercentage(this._healthComponent.CurrentHealth, this._healthComponent.MaximumHealth));
                if (this.healthPercentage <= 50 || this.healthPercentage <= 25 || this.healthPercentage <= 10)
                {
                    result = true;
                }
            }

            if (this._hungerComponent != null && this._hungerComponent.IsHungry)
            {
                result = true;
            }

            return result;
        }
        public DGBehaviourWeight GetWeight()
        {
            DGBehaviourWeight weight = new();

            // HEALTH
            if (this._healthComponent != null)
            {
                this.healthPercentage = (int)Math.Round(DGPercentageMath.GetPercentage(this._healthComponent.CurrentHealth, this._healthComponent.MaximumHealth));
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
            _ = this.descriptionStringBuilder.Clear();
            _ = this.descriptionStringBuilder.Append(DGLocalization.GetMessage_SelfPreservation_Description_Intro(this._entity.Name));
            _ = this.descriptionStringBuilder.Append(' ');

            // If health is less than 50%, start a rest.
            if (this._effectsComponent != null)
            {
                if (this.healthPercentage <= 50)
                {
                    this._effectsComponent.AddEffect<DGRestEffect>();

                    _ = this.descriptionStringBuilder.Append(DGLocalization.MESSAGES_BEHAVIOR_SELF_PRESERVATION_DESCRIPTION_HEALTH);
                    _ = this.descriptionStringBuilder.Append(' ');
                }
            }

            // If I'm hungry, I'll eat some food from the inventory.
            if (this._hungerComponent != null &&
                this._hungerComponent.IsHungry &&
                this._inventoryComponent != null)
            {
                _ = this.descriptionStringBuilder.Append(DGLocalization.MESSAGES_BEHAVIOR_SELF_PRESERVATION_DESCRIPTION_HUNGER);
                List<DGFood> foods = GetFoodFromInventory(this._inventoryComponent);

                // Feeding loop until you are satiated or the food runs out.
                while (foods.Count > 0 && this._hungerComponent.CurrentHunger > 0)
                {
                    DGFood randomFood = foods[this._game.RandomMath.Range(0, foods.Count)];
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

            DGPlayerActionInfo infos = new();
            infos.WithName(DGLocalization.MESSAGES_BEHAVIOR_SELF_PRESERVATION_NAME);
            infos.WithTitle(DGLocalization.GetMessage_SelfPreservation_Title(this._entity.Name));
            infos.WithDescription(this.descriptionStringBuilder.ToString());
            infos.WithPriorityLevel(5);
            infos.WithAuthor(this._entity.Id);
            infos.WithColor(DGColor.MediumPurple);
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
