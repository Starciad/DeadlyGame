namespace DG.Core.Components.Common
{
    internal sealed class DGSanityComponent : DGComponent
    {
        internal int CurrentSanity { get; private set; }
        internal int MaximumSanity { get; private set; }

        internal bool TemporaryInsanity { get; private set; }
        internal bool IndefiniteInsanity { get; private set; }

        internal void SetCurrentSanity(uint value)
        {
            this.CurrentSanity = (int)value;
        }

        internal void SetMaximumSanity(uint value)
        {
            this.MaximumSanity = (int)value;
        }

        internal void Decrease(uint value)
        {
            this.CurrentSanity -= (int)value;
            this.CurrentSanity = this.CurrentSanity < 0 ? 0 : this.CurrentSanity;
        }

        internal void Increase(uint value)
        {
            this.CurrentSanity += (int)value;
            this.CurrentSanity = this.CurrentSanity > this.MaximumSanity ? this.MaximumSanity : this.CurrentSanity;
        }
    }
}
