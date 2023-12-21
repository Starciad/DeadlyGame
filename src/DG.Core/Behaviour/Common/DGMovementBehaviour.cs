using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Managers;
using DG.Core.Entities;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGMovementBehaviour : IDGBehaviour
    {
        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();

            DGPersonalityComponent personality = entity.ComponentContainer.GetComponent<DGPersonalityComponent>();
            DGHungerComponent hunger = entity.ComponentContainer.GetComponent<DGHungerComponent>();

            // === ADIÇÕES ===
            // Perigo [+3.5]
            // Necessidade de Status (fome, medo, etc) [+2]
            if (hunger.IsHungry)
            {
                weight.Add(2f);
            }

            // Explorador [+1]
            if (personality.PersonalityType == DGPersonalityType.Explorer)
            {
                weight.Add(2.5f);
            }

            // Procurando Itens/Loot [+2]
            // Procurando Jogadores (Amigos) [+1]
            // Procurando Jogadores (Inimigos) [+1]
            // Procurando Recursos [+1]

            // === SUBTRAÇÕES ===
            // Tímido [-2.5]
            if (personality.CourageLevel == DGCourageLevel.Fearful)
            {
                weight.Remove(2.5f);
            }

            // Confortavel [-1.5]
            // Tem uma Base [-1]
            // Noite [-2]
            if (game.WorldManager.CurrentDaylightCycle == DGWorldDaylightCycleState.Night)
            {
                weight.Remove(2f);
            }

            return weight;
        }

        public DGBehaviourActInfos Act(DGEntity entity, DGGame game)
        {
            // Act
            DGTransformComponent transform = entity.ComponentContainer.GetComponent<DGTransformComponent>();
            DGCombatInfosComponent combatInfos = entity.ComponentContainer.GetComponent<DGCombatInfosComponent>();

            int dr = combatInfos.DisplacementRate;

            float move_x = game.Random.Range(-dr, dr);
            float move_y = game.Random.Range(-dr, dr);

            transform.Move(new(move_x, move_y));
            transform.SetPosition(game.WorldManager.Clamp(transform.Position));

            // Infos
            DGBehaviourActInfos infos = new();
            infos.WithTitle(string.Empty);
            infos.WithDescription(string.Empty);
            return infos;
        }
    }
}
