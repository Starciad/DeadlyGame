namespace DG.Core.Components.Common
{
    internal sealed class DGCombatComponent : DGComponent
    {
        internal int Initiative { get; private set; }
        internal int DisplacementRate { get; private set; }

        internal void SetInitiativeValue(int value)
        {
            this.Initiative = value;
        }

        internal void SetDisplacementRateValue(int value)
        {
            this.DisplacementRate = value;
        }
    }
}
