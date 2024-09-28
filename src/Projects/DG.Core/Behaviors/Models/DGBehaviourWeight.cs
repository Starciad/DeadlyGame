namespace DeadlyGame.Core.Behaviors.Models
{
    public struct DGBehaviourWeight
    {
        public readonly float Value => this.value;

        private float value;

        public void Add(float value)
        {
            this.value += value;
        }

        public void Remove(float value)
        {
            this.value -= value;
        }
    }
}