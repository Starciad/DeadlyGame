using System;

namespace DG.Core.Behaviour.Models
{
    internal struct DGBehaviourWeight
    {
        internal readonly float Value => this.value;

        private float value;

        internal void Add(float value)
        {
            this.value += value;
            Clamp();
        }

        internal void Remove(float value)
        {
            this.value -= value;
            Clamp();
        }

        private void Clamp()
        {
            this.value = Math.Clamp(this.value, 0, 10);
        }
    }
}
