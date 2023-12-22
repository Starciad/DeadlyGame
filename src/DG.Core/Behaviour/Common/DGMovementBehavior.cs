using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Entities;
using DG.Core.Managers;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGMovementBehavior : IDGBehaviour
    {
        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();

            // Personality
            if (entity.ComponentContainer.TryGetComponent(out DGPersonalityComponent personality))
            {
                if (personality.PersonalityType == DGPersonalityType.Explorer)
                {
                    weight.Add(3f);
                }
                else
                {
                    weight.Add(1.5f);
                }

                if (personality.CourageLevel == DGCourageLevel.Fearful)
                {
                    weight.Remove(1.5f);
                }
                else
                {
                    weight.Remove(0.5f);
                }
            }

            // Hunger
            if (entity.ComponentContainer.TryGetComponent(out DGHungerComponent hunger))
            {
                if (hunger.IsHungry)
                {
                    weight.Add(2f);
                }
            }

            // Day Time
            switch (game.WorldManager.CurrentDaylightCycle)
            {
                case DGWorldDaylightCycleState.Day:
                    weight.Add(2f);
                    break;

                case DGWorldDaylightCycleState.Night:
                    weight.Remove(1.5f);
                    break;

                default:
                    break;
            }

            // Looking for Items/Loot. [+2]
            // Looking for Resources. [+2]
            // Comfortable. [-1.5]
            // Has a Base. [-1]

            return weight;
        }
        public DGBehaviourActInfos Act(DGEntity entity, DGGame game)
        {
            // === ACT ===
            DGTransformComponent transform = entity.ComponentContainer.GetComponent<DGTransformComponent>();
            DGCombatComponent combatInfos = entity.ComponentContainer.GetComponent<DGCombatComponent>();

            int dr = combatInfos.DisplacementRate;

            float move_x = game.Random.Range(-dr, dr);
            float move_y = game.Random.Range(-dr, dr);

            transform.Move(new(move_x, move_y));
            transform.SetPosition(game.WorldManager.Clamp(transform.Position));

            // === INFOS ===
            DGBehaviourActInfos infos = new();
            infos.WithTitle(string.Empty);
            infos.WithDescription(string.Empty);
            return infos;
        }
    }
}
