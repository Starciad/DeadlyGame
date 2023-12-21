namespace DG.Core.Components.Common
{
    public sealed class DGHealthComponent : DGComponent
    {
        public bool IsDead => this.CurrentHealth <= 0;
        public int CurrentHealth { get; private set; }
        public int MaximumHealth { get; private set; }

        public void SetCurrentHealth(int value)
        {
            this.CurrentHealth = (int)value;
        }

        public void SetMaximumHealth(int value)
        {
            this.MaximumHealth = (int)value;
        }

        public void Hurt(int value)
        {
            this.CurrentHealth -= (int)value;
            this.CurrentHealth = this.CurrentHealth < 0 ? 0 : this.CurrentHealth;
        }

        public void Heal(int value)
        {
            this.CurrentHealth += (int)value;
            this.CurrentHealth = this.CurrentHealth > this.MaximumHealth ? this.MaximumHealth : this.CurrentHealth;
        }
    }
}
