using DeadlyGame.Core.Components;
using DeadlyGame.Core.Entities;

namespace DeadlyGame.Core.GameContent.Components
{
    public class DGHungerComponent : DGComponent
    {
        public bool IsHungry => this.CurrentHunger == this.MaximumHunger;
        public int CurrentHunger { get; private set; }
        public int MaximumHunger { get; private set; }

        public DGHungerComponent(DGGame game, DGEntity entity) : base(game, entity)
        {

        }

        public override void Update()
        {
            if (this.IsHungry && this.DGEntityInstance.ComponentContainer.TryGetComponent(out DGHealthComponent value))
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

        public void DecreaseHunger(int value)
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
