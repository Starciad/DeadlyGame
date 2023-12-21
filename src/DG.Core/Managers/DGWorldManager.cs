using DG.Core.Builders;
using DG.Core.Objects;

using System.Numerics;
using System.Threading.Tasks;

namespace DG.Core.Managers
{
    internal enum DGWorldDaylightCycleState
    {
        Day = 0,
        Night = 1
    }

    internal sealed class DGWorldManager : DGObject
    {
        public int CurrentDay => (int)this.currentDay;
        public DGWorldDaylightCycleState CurrentDaylightCycle => this.currentDaylightCycle;
        public Vector2 Size => this.worldSize;

        private uint currentDay;
        private DGWorldDaylightCycleState currentDaylightCycle;
        private Vector2 worldSize;

        internal async Task InitializeAsync(DGWorldBuilder builder)
        {
            this.currentDay = 1;
            this.currentDaylightCycle = DGWorldDaylightCycleState.Day;
            this.worldSize = builder.Size;
            await Task.CompletedTask;
        }

        internal async Task UpdateAsync()
        {
            UpdateDay();
            await Task.CompletedTask;
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
    }
}