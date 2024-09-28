namespace DeadlyGame.Core.Components.Common
{
    internal class DGHungerComponent : DGComponent
    {
        internal bool IsHungry => this.CurrentHunger == this.MaximumHunger;
        internal int CurrentHunger { get; private set; }
        internal int MaximumHunger { get; private set; }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (this.IsHungry && this.Entity.ComponentContainer.TryGetComponent(out DGHealthComponent value))
            {
                ApplyDamage(value);
            }

            IncreaseHunger(1);
        }

        internal void SetCurrentHunger(int value)
        {
            this.CurrentHunger = value;
        }

        internal void SetMaximumHunger(int value)
        {
            this.MaximumHunger = value;
        }

        internal void DecreaseHunger(int value)
        {
            this.CurrentHunger -= value;
            this.CurrentHunger = this.CurrentHunger < 0 ? 0 : this.CurrentHunger;
        }

        internal void IncreaseHunger(int value)
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
