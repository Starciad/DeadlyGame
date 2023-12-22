namespace DG.Core.Components.Common
{
    internal class DGHungerComponent : DGComponent
    {
        public bool IsHungry => this.CurrentHunger == this.MaximumHunger;
        public int CurrentHunger { get; private set; }
        public int MaximumHunger { get; private set; }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (this.IsHungry && this.Entity.ComponentContainer.TryGetComponent(out DGHealthComponent value))
            {
                ApplyDamage(value);
            }

            IncreaseHunger(1);
        }

        public void SetCurrentHunger(int value)
        {
            this.CurrentHunger = value;
        }

        public void SetMaximumHunger(int value)
        {
            this.MaximumHunger = value;
        }

        public void ReduceHunger(int value)
        {
            this.CurrentHunger -= value;
            this.CurrentHunger = this.CurrentHunger < 0 ? 0 : this.CurrentHunger;
        }

        public void IncreaseHunger(int value)
        {
            this.CurrentHunger += value;
            this.CurrentHunger = this.CurrentHunger > this.CurrentHunger ? this.MaximumHunger : this.CurrentHunger;
        }

        private static void ApplyDamage(DGHealthComponent health)
        {
            health.Hurt(1);
        }
    }
}
