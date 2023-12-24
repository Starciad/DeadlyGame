using DG.Core.Managers;

using System.Numerics;

namespace DG.Core.Information.World
{
    public struct DGWorldInfo
    {
        public int CurrentDay { get; set; }
        public DGWorldDaylightCycleState CurrentDaylightCycle { get; set; }
        public Vector2 WorldSize { get; set; }
        public DGWorldResourcesInfo ResourceInfo { get; set; }

        public DGWorldInfo()
        {
            this.CurrentDay = 1;
            this.CurrentDaylightCycle = DGWorldDaylightCycleState.Day;
            this.WorldSize = Vector2.Zero;
            this.ResourceInfo = new();
        }
    }
}
