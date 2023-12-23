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
    }
}
