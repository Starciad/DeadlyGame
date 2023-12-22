using DG.Core.Builders;
using DG.Core.Components.Common;
using DG.Core.Entities;
using DG.Core.Entities.Natural;
using DG.Core.Entities.Players;
using DG.Core.Items;
using DG.Core.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DG.Core.Managers
{
    internal enum DGWorldDaylightCycleState
    {
        Day = 0,
        Night = 1
    }

    internal sealed class DGWorldManager : DGObject
    {
        internal int CurrentDay => this.currentDay;
        internal DGWorldDaylightCycleState CurrentDaylightCycle => this.currentDaylightCycle;
        internal Vector2 Size => this.worldSize;

        // === Day ===
        private int currentDay;
        private DGWorldDaylightCycleState currentDaylightCycle;

        // === World General ===
        private Vector2 worldSize;

        // === World Resources ===
        private readonly List<DGEntity> resourceEntities = [];
        private readonly List<DGWorldItem> worldItems = [];

        // === System ===
        public void Initialize(DGWorldBuilder builder)
        {
            InitializeWorld(builder);
            InitializeResources(builder);
        }
        protected override void OnUpdate()
        {
            UpdateDay();
            UpdateResources();
        }

        // === Initialize ===
        private void InitializeWorld(DGWorldBuilder builder)
        {
            this.currentDay = 1;
            this.currentDaylightCycle = DGWorldDaylightCycleState.Day;
            this.worldSize = builder.Size;
        }
        private void InitializeResources(DGWorldBuilder builder)
        {
            // === RESOURCES ===
            // Trees
            for (int i = 0; i < builder.Resources.TreeCount; i++)
            {
                resourceEntities.Add(new DGTree());
            }

            // Stones
            for (int i = 0; i < builder.Resources.StoneCount; i++)
            {
                resourceEntities.Add(new DGTerrainStone());
            }

            // Bushes
            for (int i = 0; i < builder.Resources.ShrubCount; i++)
            {
                resourceEntities.Add(new DGBush());
            }

            foreach (DGEntity resourceEntity in resourceEntities)
            {
                resourceEntity.SetGameInstance(this.Game);
                resourceEntity.Initialize();
            }
        }

        // === Updates ===
        private void UpdateDay()
        {
            switch (this.currentDaylightCycle)
            {
                case DGWorldDaylightCycleState.Day:
                    this.currentDaylightCycle = DGWorldDaylightCycleState.Night;
                    break;

                case DGWorldDaylightCycleState.Night:
                    this.currentDaylightCycle = DGWorldDaylightCycleState.Day;
                    this.currentDay++;
                    break;

                default:
                    break;
            }
        }
        private void UpdateResources()
        {
            resourceEntities.RemoveAll(x => x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead);

            foreach (DGEntity resourceEntity in resourceEntities)
            {
                resourceEntity.Update();
            }
        }

        // === Utilities ===
        internal DGEntity[] GetNearbyResources(Vector2 position)
        {
            return
            [
                .. this.resourceEntities.OrderByDescending(x => Vector2.Distance(x.ComponentContainer.GetComponent<DGTransformComponent>().Position, position)),
            ];
        }
        internal DGWorldItem[] GetNearbyItems(Vector2 position)
        {
            return
            [
                .. this.worldItems.OrderByDescending(x => Vector2.Distance(x.Position, position)),
            ];
        }
        internal void AddWorldItem(DGWorldItem worldItem)
        {
            this.worldItems.Add(worldItem);
        }
        internal DGWorldItem GetWorldItem(DGItem item)
        {
            return GetWorldItem(item.GetType());
        }
        internal DGWorldItem GetWorldItem(Type itemType)
        {
            return this.worldItems.Find(x => x.Item.GetType() == itemType);
        }
        internal void RemoveWorldItem(DGWorldItem worldItem)
        {
            _ = this.worldItems.Remove(worldItem);
        }

        // === Tools ===
        internal Vector2 Clamp(Vector2 position)
        {
            float pos_x = Math.Clamp(position.X, -this.Size.X, this.Size.X);
            float pos_y = Math.Clamp(position.Y, -this.Size.Y, this.Size.Y);

            return new(pos_x, pos_y);
        }
        internal Vector2 GetRandomPosition()
        {
            float pos_x = this.Game.Random.Range(-this.Size.X, this.Size.X + 1);
            float pos_y = this.Game.Random.Range(-this.Size.Y, this.Size.Y + 1);

            return new(pos_x, pos_y);
        }
    }
}