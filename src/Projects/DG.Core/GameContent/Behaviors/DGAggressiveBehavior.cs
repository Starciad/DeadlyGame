﻿using DeadlyGame.Core.Behaviors;
using DeadlyGame.Core.Behaviors.Models;
using DeadlyGame.Core.Constants;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Enums.Items.Weapons;
using DeadlyGame.Core.Enums.Personalities;
using DeadlyGame.Core.Enums.World;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Entities.Players;
using DeadlyGame.Core.Items.Types;
using DeadlyGame.Core.Localization;
using DeadlyGame.Core.Mathematics;
using DeadlyGame.Core.Mathematics.Primitives;
using DeadlyGame.Core.Models;
using DeadlyGame.Core.Models.Infos.Actions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.Core.GameContent.Behaviors
{
    public sealed class DGAggressiveBehavior : IDGBehaviour
    {
        // System
        private DGEntity _entity;
        private DGGame _game;

        // Infos
        private DGPlayer _targetEntity;
        private DGWeapon _weaponUsed;
        private int _totalDamage;
        private int _attackTest;
        private int _attributeValue;
        private bool _isCriticalAttack;

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
            if (!TryGetNearbyTarget(out this._targetEntity))
            {
                return false;
            }

            componentChecker.Clear();
            componentChecker.Add(this._targetEntity.ComponentContainer.TryGetComponent(out this._targetEntityHealth));
            componentChecker.Add(this._targetEntity.ComponentContainer.TryGetComponent(out this._targetRelationshipsComponent));

            // Check if any of the target entity components above are null.
            return !componentChecker.Any(x => !x);
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
                double healthPercentage = Math.Round(DGPercentageMath.GetPercentage(this._healthComponent.CurrentHealth, this._healthComponent.MaximumHealth));

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
            AttemptAttack();

            // Modify Relationships
            ModifyRelationships();

            // Return Behaviour Act Infos
            DGPlayerActionInfo infos = new();

            string entityName = this._entity.Name;
            string opponentsName = this._targetEntity.Name;
            string weaponName = this._weaponUsed == null ? DGLocalization.ITEMS_WEAPONS_HAND : this._weaponUsed.Name;

            infos.WithName(DGLocalization.MESSAGES_BEHAVIOR_AGGRESSIVE_NAME);
            if (this._targetEntityHealth.IsDead)
            {
                // Death Message
                infos.WithTitle(DGLocalization.GetMessage_Aggressive_Title_Killed(entityName, opponentsName));
                infos.WithDescription(DGLocalization.GetMessage_Aggressive_Description_Killed(entityName, opponentsName, this._totalDamage, weaponName));
                infos.WithPriorityLevel(10);
                infos.WithColor(DGColor.DarkRed);
            }
            else
            {
                infos.WithTitle(DGLocalization.GetMessage_Aggressive_Title_Attack(entityName, opponentsName));
                infos.WithDescription(DGLocalization.GetMessage_Aggressive_Description_Attack(entityName, this._totalDamage, weaponName, opponentsName) + (this._isCriticalAttack ? ' ' + DGLocalization.MESSAGES_BEHAVIOR_AGGRESSIVE_IS_CRITICAL : string.Empty));
                infos.WithColor(DGColor.Red);
                infos.WithPriorityLevel(8);
            }

            infos.WithAuthor(this._entity.Id);
            infos.WithInvolved(this._targetEntity.Id);

            return infos;
        }

        private bool TryGetNearbyTarget(out DGPlayer nearbyPlayer)
        {
            foreach (DGPlayer entity in this._game.PlayerManager.GetNearbyPlayers(this._transformComponent.Position).Where(x => x != this._entity))
            {
                this._targetEntityTransform = entity.ComponentContainer.GetComponent<DGTransformComponent>();

                if (DGPoint.Distance(this._transformComponent.Position, this._targetEntityTransform.Position) <= DGInteractionsConstants.MAXIMUM_RANGE)
                {
                    nearbyPlayer = entity;
                    return true;
                }
            }

            nearbyPlayer = null;
            return false;
        }
        private void AttemptAttack()
        {
            this._weaponUsed = this._equipmentComponent.Weapon;
            if (this._weaponUsed == null)
            {
                // Attack with the hand.
                this._attackTest = DGAttributesMath.GetAttributeTestValue(this._game.Dice, this._characteristicsComponent.Strength);
                this._attributeValue = this._characteristicsComponent.Strength;
            }
            else
            {
                // Attack with the respective equipped weapon.
                switch (this._weaponUsed.WeaponType)
                {
                    case DGWeaponType.Melee:
                        this._attributeValue = this._characteristicsComponent.Strength;
                        this._attackTest = DGAttributesMath.GetAttributeTestValue(this._game.Dice, this._attributeValue);
                        break;

                    case DGWeaponType.Ranged:
                        this._attributeValue = this._characteristicsComponent.Dexterity;
                        this._attackTest = DGAttributesMath.GetAttributeTestValue(this._game.Dice, this._attributeValue);
                        break;

                    case DGWeaponType.Magic:
                        this._attributeValue = this._characteristicsComponent.Intelligence;
                        this._attackTest = DGAttributesMath.GetAttributeTestValue(this._game.Dice, this._attributeValue);
                        break;

                    default:
                        this._attributeValue = this._characteristicsComponent.Strength;
                        this._attackTest = DGAttributesMath.GetAttributeTestValue(this._game.Dice, this._attributeValue);
                        break;
                }
            }

            // Check if it was a critical value
            this._isCriticalAttack = DGAttributesMath.IsMaxAttributeValueInTest(this._attributeValue, this._attackTest);
            this._totalDamage = this._combatComponent.GetFullAttackDamage(this._isCriticalAttack);

            this._targetEntityHealth.Hurt(this._totalDamage);
        }
        private void ModifyRelationships()
        {
            this._relationshipsComponent.DecreaseRelationshipValue(this._targetEntity, 20);
            this._targetRelationshipsComponent.DecreaseRelationshipValue(this._entity, 20);
        }
    }
}