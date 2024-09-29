using DeadlyGame.Core.Components;
using DeadlyGame.Core.Entities;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGCombatComponent : DGComponent
    {
        public int DisplacementRate { get; private set; }

        public DGCombatComponent(DGGame game, DGEntity entity) : base(game, entity)
        {

        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
        }

        public void SetDisplacementRateValue(int value)
        {
            this.DisplacementRate = value;
        }

        public int GetFullAttackDamage(bool isCritical)
        {
            int attackValue = 0;

            // Apply base weapon damage
            if (this.DGEntityInstance.ComponentContainer.TryGetComponent(out DGEquipmentComponent value))
            {
                attackValue = value.Weapon == null ? 2 : this.DGGameInstance.Dice.Roll(value.Weapon.Damage);
            }

            // Apply Buff Modifiers
            // Apply Accessory Modifiers
            // Apply Armor modifiers
            // Apply critical damage
            if (isCritical)
            {
                attackValue *= 2;
            }

            return attackValue;
        }
    }
}
