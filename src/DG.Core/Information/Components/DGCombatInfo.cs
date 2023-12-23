using DG.Core.Components.Common;

namespace DG.Core.Information.Components
{
    public struct DGCombatInfo
    {
        public int DisplacementRate { get; set; }

        internal static DGCombatInfo Create(DGCombatComponent component)
        {
            return new()
            {
                DisplacementRate = component.DisplacementRate,
            };
        }
    }
}
