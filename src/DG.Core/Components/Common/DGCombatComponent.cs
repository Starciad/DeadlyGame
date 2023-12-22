using DG.Core.Entities;

namespace DG.Core.Components.Common
{
    internal sealed class DGCombatComponent : DGComponent
    {
        internal int DisplacementRate { get; private set; }

        internal void SetDisplacementRateValue(int value)
        {
            this.DisplacementRate = value;
        }
    }
}
