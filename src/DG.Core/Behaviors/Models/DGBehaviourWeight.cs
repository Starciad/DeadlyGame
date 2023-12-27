namespace DG.Core.Behaviors.Models
{
    internal struct DGBehaviourWeight
    {
        internal readonly float Value => this.value;

        private float value;

        internal void Add(float value)
        {
            this.value += value;
        }

        internal void Remove(float value)
        {
            this.value -= value;
        }
    }
}