namespace DG.Core.Components.Common
{
    internal sealed class DGCombatComponent : DGComponent
    {
        internal int DisplacementRate { get; private set; }

        internal void SetDisplacementRateValue(int value)
        {
            this.DisplacementRate = value;
        }

        internal int GetFullAttackDamage(bool isCritical)
        {
            int attackValue = 0;

            // Apply base weapon damage
            if (this.Entity.ComponentContainer.TryGetComponent(out DGEquipmentComponent value))
            {
                if (value.Weapon == null)
                {
                    attackValue = 1;
                }
                else
                {
                    attackValue = this.Game.Dice.Roll(value.Weapon.Damage);
                }
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
