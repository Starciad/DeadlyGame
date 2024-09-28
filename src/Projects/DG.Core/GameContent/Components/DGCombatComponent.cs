using DeadlyGame.Core.Components;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGCombatComponent : DGComponent
    {
        public int DisplacementRate { get; private set; }

        public void SetDisplacementRateValue(int value)
        {
            this.DisplacementRate = value;
        }

        public int GetFullAttackDamage(bool isCritical)
        {
            int attackValue = 0;

            // Apply base weapon damage
            if (this.Entity.ComponentContainer.TryGetComponent(out DGEquipmentComponent value))
            {
                attackValue = value.Weapon == null ? 2 : this.Game.Dice.Roll(value.Weapon.Damage);
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
