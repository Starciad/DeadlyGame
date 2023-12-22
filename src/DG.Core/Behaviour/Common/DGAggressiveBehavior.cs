using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Constants;
using DG.Core.Dice;
using DG.Core.Entities;
using DG.Core.Entities.Players;
using DG.Core.Items.Weapons;
using DG.Core.Managers;
using DG.Core.Utilities;

using System;
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
                    weight.Add(1.5f);
                    break;

                default:
                    break;
            }

            // Hunger
            if (entity.ComponentContainer.TryGetComponent(out DGHungerComponent hunger))
            {
                if (hunger.IsHungry)
                {
                    weight.Add(2.5f);
                }
            }

            // Health
            if (entity.ComponentContainer.TryGetComponent(out DGHealthComponent health))
            {
                double healthPercentage = Math.Round(DGPercentageUtilities.CalculatePercentage(health.CurrentHealth, health.MaximumHealth));

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
        public DGBehaviourActInfos Act(DGEntity entity, DGGame game)
        {
            DGBehaviourActInfos infos = new();

            // Get Nearby Target
            DGPlayer targetEntity = GetNearbyTarget(entity, game);
            if (targetEntity == null)
            {
                infos.WithTitle("No Target Found");
                infos.WithDescription("No nearby targets found to attack.");
                return infos;
            }

            // Attack Calculation
            AttackOutcome attackResult = AttemptAttack(game.Dice, entity, targetEntity, game);

            // Modify Relationships
            ModifyRelationships(entity, targetEntity);

            // Handle Attack Outcome
            infos = HandleAttackOutcome(attackResult, targetEntity);

            // Return Behaviour Act Infos
            return infos;
        }

        private static DGPlayer GetNearbyTarget(DGEntity sourceEntity, DGGame currentGame)
        {
            foreach (DGPlayer anotherEntity in currentGame.PlayerManager.GetNearbyPlayers(sourceEntity.ComponentContainer.GetComponent<DGTransformComponent>().Position))
            {
                var currentTransform = sourceEntity.ComponentContainer.GetComponent<DGTransformComponent>();
                var anotherTransform = anotherEntity.ComponentContainer.GetComponent<DGTransformComponent>();

                if (Vector2.Distance(currentTransform.Position, anotherTransform.Position) <= DGInteractionsConstants.MAXIMUM_RANGE)
                {
                    return anotherEntity;
                }
            }
            return null;
        }
        private static AttackOutcome AttemptAttack(DGDice dice, DGEntity attacker, DGEntity target, DGGame currentGame)
        {
            var attackerWeapon = attacker.ComponentContainer.GetComponent<DGEquipmentComponent>();
            var attackerCharacteristics = attacker.ComponentContainer.GetComponent<DGCharacteristicsComponent>();
            var attackerCombat = attacker.ComponentContainer.GetComponent<DGCombatComponent>();

            var targetEquipment = target.ComponentContainer.GetComponent<DGEquipmentComponent>();
            var targetHealth = target.ComponentContainer.GetComponent<DGHealthComponent>();

            int totalDamage;
            int attackTest;
            int attributeValue;

            if (attackerWeapon.Weapon == null)
            {
                // Attack with the hand.
                attackTest = DGAttributesUtilities.GetAttributeTestValue(dice, attackerCharacteristics.Strength);
                attributeValue = attackerCharacteristics.Strength;
            }
            else
            {
                // Attack with the respective equipped weapon.
                switch (attackerWeapon.Weapon.WeaponType)
                {
                    case DGWeaponType.Melee:
                        attributeValue = attackerCharacteristics.Strength;
                        attackTest = DGAttributesUtilities.GetAttributeTestValue(dice, attributeValue);
                        break;

                    case DGWeaponType.Ranged:
                        attributeValue = attackerCharacteristics.Dexterity;
                        attackTest = DGAttributesUtilities.GetAttributeTestValue(dice, attributeValue);
                        break;

                    case DGWeaponType.Magic:
                        attributeValue = attackerCharacteristics.Intelligence;
                        attackTest = DGAttributesUtilities.GetAttributeTestValue(dice, attributeValue);
                        break;

                    default:
                        attributeValue = attackerCharacteristics.Strength;
                        attackTest = DGAttributesUtilities.GetAttributeTestValue(dice, attributeValue);
                        break;
                }
            }

            // Check if it was a critical value
            totalDamage = attackerCombat.GetFullAttackDamage(DGAttributesUtilities.IsMaxAttributeValueInTest(attributeValue, attackTest));

            // Verify that the current attack roll was greater than or equal to the target's armor class value.
            if (attackTest >= targetEquipment.GetArmoredClass())
            {
                targetHealth.Hurt(totalDamage);
                return AttackOutcome.Successful;
            }

            // If not, the attack is seen as a failure.
            return AttackOutcome.Failed;
        }
        private static void ModifyRelationships(DGEntity attacker, DGEntity target)
        {
            var attackerRelationship = attacker.ComponentContainer.GetComponent<DGRelationshipsComponent>();
            var targetRelationship = target.ComponentContainer.GetComponent<DGRelationshipsComponent>();

            attackerRelationship.DecreaseRelationshipValue(target, 20);
            targetRelationship.DecreaseRelationshipValue(attacker, 20);
        }
        private static DGBehaviourActInfos HandleAttackOutcome(AttackOutcome result, DGEntity target)
        {
            DGBehaviourActInfos outcomeInfo = new();
            switch (result)
            {
                case AttackOutcome.Successful:
                    outcomeInfo.WithTitle("Attack Successful");
                    outcomeInfo.WithDescription($"Dealt damage to {target.Name}.");
                    break;
                case AttackOutcome.Failed:
                    outcomeInfo.WithTitle("Attack Failed");
                    outcomeInfo.WithDescription($"Missed the attack on {target.Name}.");
                    break;
            }

            return outcomeInfo;
        }
    }
}