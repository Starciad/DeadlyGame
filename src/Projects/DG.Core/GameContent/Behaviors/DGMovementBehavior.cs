using DeadlyGame.Core.Behaviors;
using DeadlyGame.Core.Behaviors.Models;
using DeadlyGame.Core.Constants;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Enums.Personalities;
using DeadlyGame.Core.Enums.World;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.Localization;
using DeadlyGame.Core.Mathematics.Primitives;
using DeadlyGame.Core.Models;
using DeadlyGame.Core.Models.Infos.Actions;

namespace DeadlyGame.Core.GameContent.Behaviors
{
    public sealed class DGMovementBehavior : IDGBehaviour
    {
        // System
        private DGEntity _entity;
        private DGGame _game;

        // Components
        private DGTransformComponent _transformComponent;
        private DGPersonalityComponent _personalityComponent;
        private DGHungerComponent _hungerComponent;
        private DGCombatComponent _combatComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._entity = entity;
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
            DGPoint oldPos = this._transformComponent.Position;

            // === ACT ===
            int dr = this._combatComponent.DisplacementRate;

            int move_x = this._game.RandomMath.Range(-dr, dr);
            int move_y = this._game.RandomMath.Range(-dr, dr);

            this._transformComponent.Move(new(move_x, move_y));
            this._transformComponent.SetPosition(this._game.WorldManager.Clamp(this._transformComponent.Position));

            DGPoint newPos = this._transformComponent.Position;

            // === INFOS ===
            DGPlayerActionInfo infos = new();
            infos.WithName(DGLocalization.Read(DGBehaviourConstants.S_MOVEMENT_BEHAVIOR, "Name"));
            infos.WithTitle(string.Format(DGLocalization.Read(DGBehaviourConstants.S_MOVEMENT_BEHAVIOR, "Title"), this._entity.Name));
            infos.WithDescription(string.Format(DGLocalization.Read(DGBehaviourConstants.S_MOVEMENT_BEHAVIOR, "Description"), this._entity.Name, oldPos.X, oldPos.Y, newPos.X, newPos.Y));
            infos.WithPriorityLevel(4);
            infos.WithAuthor(this._entity.Id);
            infos.WithColor(DGColor.LightCyan);
            return infos;
        }
    }
}
