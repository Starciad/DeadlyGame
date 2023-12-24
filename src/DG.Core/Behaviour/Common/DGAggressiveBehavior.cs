using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Constants;
using DG.Core.Entities;
using DG.Core.Entities.Players;
using DG.Core.Information.Actions;
using DG.Core.Items.Weapons;
using DG.Core.Managers;
using DG.Core.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGAggressiveBehavior : IDGBehaviour
    {
        internal enum AttackOutcome
        {
            Successful,
            Failed
        }

        // System
        private DGEntity _entity;
        private DGGame _game;

        // Infos
        private DGPlayer _targetEntity;

        // Components
        private DGTransformComponent _transformComponent;
        private DGPersonalityComponent _personalityComponent;
        private DGHealthComponent _healthComponent;
        private DGHungerComponent _hungerComponent;
        private DGEquipmentComponent _equipmentComponent;
        private DGCharacteristicsComponent _characteristicsComponent;
        private DGCombatComponent _combatComponent;
        private DGRelationshipsComponent _relationshipsComponent;

        private DGTransformComponent _targetEntityTransform;
        private DGEquipmentComponent _targetEquipmentComponent;
        private DGHealthComponent _targetEntityHealth;
        private DGRelationshipsComponent _targetRelationshipsComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._entity = entity;
            this._game = game;

            List<bool> componentChecker = [];
            componentChecker.Add(entity.ComponentContainer.TryGetComponent(out this._transformComponent));
            componentChecker.Add(entity.ComponentContainer.TryGetComponent(out this._personalityComponent));
            componentChecker.Add(entity.ComponentContainer.TryGetComponent(out this._healthComponent));
            componentChecker.Add(entity.ComponentContainer.TryGetComponent(out this._hungerComponent));
            componentChecker.Add(entity.ComponentContainer.TryGetComponent(out this._equipmentComponent));
            componentChecker.Add(entity.ComponentContainer.TryGetComponent(out this._characteristicsComponent));
            componentChecker.Add(entity.ComponentContainer.TryGetComponent(out this._combatComponent));
            componentChecker.Add(entity.ComponentContainer.TryGetComponent(out this._relationshipsComponent));

            // Check if any component above is null.
            if (componentChecker.Any(x => !x))
            {
                return false;
            }

            // Get a target entity.
            this._targetEntity = GetNearbyTarget();
            if (this._targetEntity == null)
            {
                return false;
            }

            componentChecker.Clear();
            componentChecker.Add(this._targetEntity.ComponentContainer.TryGetComponent(out this._targetEquipmentComponent));
            componentChecker.Add(this._targetEntity.ComponentContainer.TryGetComponent(out this._targetEntityHealth));
            componentChecker.Add(this._targetEntity.ComponentContainer.TryGetComponent(out this._targetRelationshipsComponent));

            // Check if any of the target entity components above are null.
            if (componentChecker.Any(x => !x))
            {
                return false;
            }

            return true;
        }
        public DGBehaviourWeight GetWeight()
        {
            DGBehaviourWeight weight = new();

            // Personality
            if (this._personalityComponent != null)
            {
                switch (this._personalityComponent.PersonalityType)
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
            switch (this._game.WorldManager.CurrentDaylightCycle)
            {
                case DGWorldDaylightCycleState.Day:
                    weight.Add(0.5f);
                    break;

                case DGWorldDaylightCycleState.Night:
                    weight.Add(1.5f);
                    break;

                default:
                    break;
            }

            // Hunger
            if (this._hungerComponent != null)
            {
                if (this._hungerComponent.IsHungry)
                {
                    weight.Add(2.5f);
                }
            }

            // Health
            if (this._healthComponent != null)
            {
                double healthPercentage = Math.Round(DGPercentageUtilities.CalculatePercentage(this._healthComponent.CurrentHealth, this._healthComponent.MaximumHealth));

                if (healthPercentage < 50)
                {
                    weight.Add(1f);
                }

                if (healthPercentage < 25)
                {
                    weight.Add(1f);
                }

                if (healthPercentage < 10)
                {
                    weight.Add(2.5f);
                }
            }

            return weight;
        }
        public DGPlayerActionInfo Act()
        {
            // Attack Calculation
            AttackOutcome attackResult = AttemptAttack();

            // Modify Relationships
            ModifyRelationships();

            // Handle Attack Outcome
            DGPlayerActionInfo infos = HandleAttackOutcome(attackResult);

            // Return Behaviour Act Infos
            return infos;
        }

        private DGPlayer GetNearbyTarget()
        {
            foreach (DGPlayer entity in this._game.PlayerManager.GetNearbyPlayers(this._transformComponent.Position).Where(x => x != this._entity))
            {
                this._targetEntityTransform = entity.ComponentContainer.GetComponent<DGTransformComponent>();

                if (Vector2.Distance(this._transformComponent.Position, this._targetEntityTransform.Position) <= DGInteractionsConstants.MAXIMUM_RANGE)
                {
                    return entity;
                }
            }

            return null;
        }
        private AttackOutcome AttemptAttack()
        {
            int totalDamage;
            int attackTest;
            int attributeValue;

            if (this._equipmentComponent.Weapon == null)
            {
                // Attack with the hand.
                attackTest = DGAttributesUtilities.GetAttributeTestValue(this._game.Dice, this._characteristicsComponent.Strength);
                attributeValue = this._characteristicsComponent.Strength;
            }
            else
            {
                // Attack with the respective equipped weapon.
                switch (this._equipmentComponent.Weapon.WeaponType)
                {
                    case DGWeaponType.Melee:
                        attributeValue = this._characteristicsComponent.Strength;
                        attackTest = DGAttributesUtilities.GetAttributeTestValue(this._game.Dice, attributeValue);
                        break;

                    case DGWeaponType.Ranged:
                        attributeValue = this._characteristicsComponent.Dexterity;
                        attackTest = DGAttributesUtilities.GetAttributeTestValue(this._game.Dice, attributeValue);
                        break;

                    case DGWeaponType.Magic:
                        attributeValue = this._characteristicsComponent.Intelligence;
                        attackTest = DGAttributesUtilities.GetAttributeTestValue(this._game.Dice, attributeValue);
                        break;

                    default:
                        attributeValue = this._characteristicsComponent.Strength;
                        attackTest = DGAttributesUtilities.GetAttributeTestValue(this._game.Dice, attributeValue);
                        break;
                }
            }

            // Check if it was a critical value
            totalDamage = this._combatComponent.GetFullAttackDamage(DGAttributesUtilities.IsMaxAttributeValueInTest(attributeValue, attackTest));

            // Verify that the current attack roll was greater than or equal to the target's armor class value.
            if (attackTest >= this._targetEquipmentComponent.GetArmoredClass())
            {
                this._targetEntityHealth.Hurt(totalDamage);
                return AttackOutcome.Successful;
            }

            // If not, the attack is seen as a failure.
            return AttackOutcome.Failed;
        }
        private void ModifyRelationships()
        {
            this._relationshipsComponent.DecreaseRelationshipValue(this._targetEntity, 20);
            this._targetRelationshipsComponent.DecreaseRelationshipValue(this._entity, 20);
        }
        private DGPlayerActionInfo HandleAttackOutcome(AttackOutcome result)
        {
            DGPlayerActionInfo outcomeInfo = new();
            switch (result)
            {
                case AttackOutcome.Successful:
                    outcomeInfo.WithTitle("Attack Successful");
                    outcomeInfo.WithDescription($"Dealt damage to {this._targetEntity.Name}.");
                    break;
                case AttackOutcome.Failed:
                    outcomeInfo.WithTitle("Attack Failed");
                    outcomeInfo.WithDescription($"Missed the attack on {this._targetEntity.Name}.");
                    break;
            }

            return outcomeInfo;
        }
    }
}