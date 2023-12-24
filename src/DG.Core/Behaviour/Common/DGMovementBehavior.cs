using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Entities;
using DG.Core.Information.Actions;
using DG.Core.Managers;

using System.Collections.Generic;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGMovementBehavior : IDGBehaviour
    {
        // System
        private DGGame _game;

        // Components
        private DGTransformComponent _transformComponent;
        private DGPersonalityComponent _personalityComponent;
        private DGHungerComponent _hungerComponent;
        private DGCombatComponent _combatComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._game = game;

            if (!entity.ComponentContainer.TryGetComponent(out this._transformComponent))
            {
                return false;
            }

            if (!entity.ComponentContainer.TryGetComponent(out this._combatComponent))
            {
                return false;
            }

            this._personalityComponent = entity.ComponentContainer.GetComponent<DGPersonalityComponent>();
            this._hungerComponent = entity.ComponentContainer.GetComponent<DGHungerComponent>();

            return true;
        }
        public DGBehaviourWeight GetWeight()
        {
            DGBehaviourWeight weight = new();

            // Personality
            if (this._personalityComponent != null)
            {
                if (this._personalityComponent.PersonalityType == DGPersonalityType.Explorer)
                {
                    weight.Add(3f);
                }
                else
                {
                    weight.Add(1.5f);
                }

                if (this._personalityComponent.CourageLevel == DGCourageLevel.Fearful)
                {
                    weight.Remove(1.5f);
                }
                else
                {
                    weight.Remove(0.5f);
                }
            }

            // Hunger
            if (this._hungerComponent != null)
            {
                if (this._hungerComponent.IsHungry)
                {
                    weight.Add(2f);
                }
            }

            // Day Time
            switch (this._game.WorldManager.CurrentDaylightCycle)
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
        public DGPlayerActionInfo Act()
        {
            // === ACT ===
            int dr = this._combatComponent.DisplacementRate;

            float move_x = this._game.Random.Range(-dr, dr);
            float move_y = this._game.Random.Range(-dr, dr);

            this._transformComponent.Move(new(move_x, move_y));
            this._transformComponent.SetPosition(this._game.WorldManager.Clamp(this._transformComponent.Position));

            // === INFOS ===
            DGPlayerActionInfo infos = new();
            infos.WithTitle(string.Empty);
            infos.WithDescription(string.Empty);
            return infos;
        }
    }
}
