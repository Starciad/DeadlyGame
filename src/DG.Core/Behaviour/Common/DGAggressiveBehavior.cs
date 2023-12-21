using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Entities;
using DG.Core.Managers;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGAggressiveBehavior : IDGBehaviour
    {
        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();

            // Personality
            if (entity.ComponentContainer.TryGetComponent(out DGPersonalityComponent personality))
            {
                switch (personality.PersonalityType)
                {
                    case DGPersonalityType.Aggressive:
                        weight.Add(2.5f);
                        break;

                    case DGPersonalityType.Impulsive:
                        weight.Add(3f);
                        break;

                    default:
                        weight.Add(1f);
                        break;
                }
            }

            // Day Time
            switch (game.WorldManager.CurrentDaylightCycle)
            {
                case DGWorldDaylightCycleState.Day:
                    weight.Add(0.5f);
                    break;

                case DGWorldDaylightCycleState.Night:
                    weight.Remove(1.5f);
                    break;

                default:
                    break;
            }

            // Hunger
            if (entity.ComponentContainer.TryGetComponent(out DGHungerComponent hunger))
            {
                if (hunger.IsHungry)
                {
                    weight.Add(1f);
                }
            }

            return weight;
        }

        public DGBehaviourActInfos Act(DGEntity entity, DGGame game)
        {
            // Infos
            DGBehaviourActInfos infos = new();
            infos.WithTitle(string.Empty);
            infos.WithDescription(string.Empty);
            return infos;
        }
    }
}
