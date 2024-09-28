namespace DeadlyGame.Core.Components.Common
{
    internal sealed class DGHealthComponent : DGComponent
    {
        internal bool IsDead => this.CurrentHealth <= 0;
        internal int CurrentHealth { get; private set; }
        internal int MaximumHealth { get; private set; }

        internal delegate void DiedEventHandler();
        internal event DiedEventHandler OnDied;

        internal void SetCurrentHealth(int value)
        {
            this.CurrentHealth = value;
        }

        internal void SetMaximumHealth(int value)
        {
            this.MaximumHealth = value;
        }

        internal void Hurt(int value)
        {
            this.CurrentHealth -= value;
            this.CurrentHealth = this.CurrentHealth < 0 ? 0 : this.CurrentHealth;

            // Check Death
            if (this.CurrentHealth == 0)
            {
                OnDied?.Invoke();
            }
        }

        internal void Heal(int value)
        {
            this.CurrentHealth += value;
            this.CurrentHealth = this.CurrentHealth > this.MaximumHealth ? this.MaximumHealth : this.CurrentHealth;
        }
    }
}
