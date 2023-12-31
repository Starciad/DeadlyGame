﻿using DG.Core.Behaviors.Models;
using DG.Core.Components.Common;
using DG.Core.Effects.Common;
using DG.Core.Entities;
using DG.Core.Information.Actions;
using DG.Core.Items;
using DG.Core.Items.Foods;
using DG.Core.Localization;
using DG.Core.Utilities;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DG.Core.Behaviors.Common
{
    internal sealed class DGSelfPreservationBehavior : IDGBehaviour
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

        // Consts
        private const string S_SELF_PRESERVATION_BEHAVIOR = "Self_Preservation";

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
            this.descriptionStringBuilder.Clear();
            this.descriptionStringBuilder.AppendFormat(DGLocalization.Read(S_SELF_PRESERVATION_BEHAVIOR, "Description_Intro"), this._entity.Name);
            this.descriptionStringBuilder.Append(' ');

            // If health is less than 50%, start a rest.
            if (this._effectsComponent != null)
            {
                if (this.healthPercentage <= 50)
                {
                    this._effectsComponent.AddEffect<DGRestEffect>();

                    this.descriptionStringBuilder.AppendFormat(DGLocalization.Read(S_SELF_PRESERVATION_BEHAVIOR, "Description_Health"));
                    this.descriptionStringBuilder.Append(' ');
                }
            }

            // If I'm hungry, I'll eat some food from the inventory.
            if (this._hungerComponent != null &&
                this._inventoryComponent != null)
            {
                this.descriptionStringBuilder.Append(DGLocalization.Read(S_SELF_PRESERVATION_BEHAVIOR, "Description_Hunger"));
                List<DGFood> foods = GetFoodFromInventory(this._inventoryComponent);

                // Feeding loop until you are satiated or the food runs out.
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

            DGPlayerActionInfo infos = new();
            infos.WithName(DGLocalization.Read(S_SELF_PRESERVATION_BEHAVIOR, "Name"));
            infos.WithTitle(string.Format(DGLocalization.Read(S_SELF_PRESERVATION_BEHAVIOR, "Title"), this._entity.Name));
            infos.WithDescription(this.descriptionStringBuilder.ToString());
            infos.WithPriorityLevel(5);
            infos.WithAuthor(this._entity.Id);
            infos.WithColor(Color.MediumPurple);
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
