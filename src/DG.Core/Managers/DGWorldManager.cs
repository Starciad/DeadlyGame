using DG.Core.Builders;
using DG.Core.Entities.Natural;
using DG.Core.Objects;

using System;
using System.Collections.Generic;
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

        private int currentDay;
        private DGWorldDaylightCycleState currentDaylightCycle;
        private Vector2 worldSize;

        // World Resources
        private readonly List<DGTree> trees = [];
        private readonly List<DGTerrainStone> stones = [];
        private readonly List<DGBush> bushes = [];

        public void Initialize(DGWorldBuilder builder)
        {
            this.currentDay = 1;
            this.currentDaylightCycle = DGWorldDaylightCycleState.Day;
            this.worldSize = builder.Size;

            // === RESOURCES ===
            // Trees
            for (int i = 0; i < builder.Resources.TreeCount; i++)
            {
                trees.Add(new());
            }

            // Stones
            for (int i = 0; i < builder.Resources.StoneCount; i++)
            {
                stones.Add(new());
            }

            // Bushes
            for (int i = 0; i < builder.Resources.ShrubCount; i++)
            {
                bushes.Add(new());
            }
        }

        public override void Update()
        {
            UpdateDay();
        }

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