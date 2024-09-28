using DeadlyGame.Core.Builders;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Enums.World;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Entities.Natural;
using DeadlyGame.Core.Items;
using DeadlyGame.Core.Mathematics.Primitives;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.Core.Managers
{
    public sealed class DGWorldManager : DGManager
    {
        public int CurrentDay => this.currentDay;
        public DGWorldDaylightCycleState CurrentDaylightCycle => this.currentDaylightCycle;
        public DGPoint Size => this.worldSize;

        // === Day ===
        private int currentDay;
        private DGWorldDaylightCycleState currentDaylightCycle;

        // === World General ===
        private DGPoint worldSize;

        // === World Resources ===
        private readonly List<DGEntity> resourceEntities = [];
        private readonly List<DGWorldItem> worldItems = [];

        public DGWorldManager(DGGame game, DGWorldBuilder builder) : base(game)
        {
            InitializeWorld(builder);
            InitializeResources(builder);
        }

        public override void Start()
        {
            return;
        }

        public override void Update()
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
                this.resourceEntities.Add(new DGTree(this.DGGameInstance));
            }

            // Stones
            for (int i = 0; i < builder.Resources.StoneCount; i++)
            {
                this.resourceEntities.Add(new DGTerrainStone(this.DGGameInstance));
            }

            // Bushes
            for (int i = 0; i < builder.Resources.ShrubCount; i++)
            {
                this.resourceEntities.Add(new DGBush(this.DGGameInstance));
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
            _ = this.resourceEntities.RemoveAll(x => x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead);

            foreach (DGEntity resourceEntity in this.resourceEntities)
            {
                resourceEntity.Update();
            }
        }

        // === Utilities ===
        public DGEntity[] GetNearbyResources(DGPoint position)
        {
            return
            [
                .. this.resourceEntities.OrderByDescending(x => DGPoint.Distance(x.ComponentContainer.GetComponent<DGTransformComponent>().Position, position)),
            ];
        }
        public DGWorldItem[] GetNearbyItems(DGPoint position)
        {
            return
            [
                .. this.worldItems.OrderByDescending(x => DGPoint.Distance(x.Position, position)),
            ];
        }
        public void AddWorldItem(DGWorldItem worldItem)
        {
            this.worldItems.Add(worldItem);
        }
        public DGWorldItem GetWorldItem(DGItem item)
        {
            return GetWorldItem(item.GetType());
        }
        public DGWorldItem GetWorldItem(Type itemType)
        {
            return this.worldItems.Find(x => x.Item.GetType() == itemType);
        }
        public void RemoveWorldItem(DGWorldItem worldItem)
        {
            _ = this.worldItems.Remove(worldItem);
        }

        // === Tools ===
        public DGPoint Clamp(DGPoint position)
        {
            int pos_x = Math.Clamp(position.X, -this.Size.X, this.Size.X);
            int pos_y = Math.Clamp(position.Y, -this.Size.Y, this.Size.Y);

            return new(pos_x, pos_y);
        }
        public DGPoint GetRandomPosition()
        {
            int pos_x = this.DGGameInstance.RandomMath.Range(-this.Size.X, this.Size.X + 1);
            int pos_y = this.DGGameInstance.RandomMath.Range(-this.Size.Y, this.Size.Y + 1);

            return new(pos_x, pos_y);
        }
    }
}