namespace DG.Core.Components.Common
{
    public sealed class DGHealthComponent : DGComponent
    {
        public bool IsDead => this.CurrentHealth <= 0;
        public int CurrentHealth { get; private set; }
        public int MaximumHealth { get; private set; }

        public delegate void DiedEventHandler();
        public event DiedEventHandler OnDied;

        public void SetCurrentHealth(int value)
        {
            this.CurrentHealth = value;
        }

        public void SetMaximumHealth(int value)
        {
            this.MaximumHealth = value;
        }

        public void Hurt(int value)
        {
            this.CurrentHealth -= value;
            this.CurrentHealth = this.CurrentHealth < 0 ? 0 : this.CurrentHealth;

            // Check Death
            if (this.CurrentHealth == 0)
            {
                OnDied?.Invoke();
            }
        }

        public void Heal(int value)
        {
            this.CurrentHealth += value;
            this.CurrentHealth = this.CurrentHealth > this.MaximumHealth ? this.MaximumHealth : this.CurrentHealth;
        }
    }
}
