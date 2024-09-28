using DeadlyGame.Core.Enums.World;
using DeadlyGame.Core.Information.Utils;

namespace DeadlyGame.Core.Information.World
{
    public struct DGWorldInfo
    {
        public int CurrentDay { get; set; }
        public DGWorldDaylightCycleState CurrentDaylightCycle { get; set; }
        public DGVector2 WorldSize { get; set; }
        public DGWorldResourcesInfo ResourceInfo { get; set; }

        public DGWorldInfo()
        {
            this.CurrentDay = 1;
            this.CurrentDaylightCycle = DGWorldDaylightCycleState.Day;
            this.WorldSize = new();
            this.ResourceInfo = new();
        }
    }
}
