using DeadlyGame.Core.Effects;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Enums.Effects;
using DeadlyGame.Core.GameContent.Components;

namespace DeadlyGame.Core.GameContent.Effects
{
    public class DGRestEffect : DGEffect
    {
        protected override void Build()
        {
            this.Name = "Descanço";
            this.Description = "Você sente suas energias retornando gradualmente.";
            this.EffectType = DGEffectType.Positive;
            this.Durability = 3;
        }

        protected override void OnApplyEffect(DGEntity entity)
        {
            if (entity.ComponentContainer.TryGetComponent(out DGHealthComponent healthComponent))
            {
                if (this.Game.Random.Chance(50, 100))
                {
                    healthComponent.Heal(1);
                }
            }
        }
    }
}
