﻿using DG.Core.Behaviour.Common;
using DG.Core.Builders;
using DG.Core.Components.Common;
using DG.Core.Utilities;

namespace DG.Core.Entities.Players
{
    internal sealed class DGPlayer : DGEntity
    {
        internal int Index { get; private set; }

        // Components
        private DGBehaviourComponent _behaviour;

        internal DGPlayer(DGPlayerBuilder builder, int index)
        {
            this.Name = builder.Name;
            this.Index = index;
        }

        private DGTransformComponent _transform;
        private DGInformationsComponent _informations;
        private DGPersonalityComponent _personality;
        private DGCharacteristicsComponent _characteristics;
        private DGHealthComponent _health;
        private DGHungerComponent _hunger;
        private DGCombatComponent _combatInfos;

        protected override void OnAwake()
        {
            base.OnAwake();

            this._transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            this._informations = this.ComponentContainer.AddComponent<DGInformationsComponent>();
            this._personality = this.ComponentContainer.AddComponent<DGPersonalityComponent>();
            this._characteristics = this.ComponentContainer.AddComponent<DGCharacteristicsComponent>();
            this._health = this.ComponentContainer.AddComponent<DGHealthComponent>();
            this._hunger = this.ComponentContainer.AddComponent<DGHungerComponent>();
            this._combatInfos = this.ComponentContainer.AddComponent<DGCombatComponent>();

            _ = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            _ = this.ComponentContainer.AddComponent<DGEquipmentComponent>();
            _ = this.ComponentContainer.AddComponent<DGRelationshipsComponent>();
        }
        protected override void OnStart()
        {
            base.OnStart();

            // ===== COMPONENTS =====
            // Transform
            this._transform.SetPosition(this.Game.WorldManager.GetRandomPosition());

            // Infos
            this._informations.Randomize();

            // Personality
            this._personality.Randomize();

            // Characteristics
            this._characteristics.Randomize();

            // Health
            this._health.SetMaximumHealth(10 + DGAttributesUtilities.GetAttributeModifier(this._characteristics.Constitution));
            this._health.SetCurrentHealth(this._health.MaximumHealth);

            // Hunger
            this._hunger.SetMaximumHunger(100);
            this._hunger.SetCurrentHunger(0);

            // Combat
            this._combatInfos.SetDisplacementRateValue(9 + this.Game.Random.Range(-2, 3));

            // Behaviour
            this._behaviour = this.ComponentContainer.AddComponent<DGBehaviourComponent>();
            this._behaviour.RegisterBehaviour(new DGMovementBehavior());
            this._behaviour.RegisterBehaviour(new DGAggressiveBehavior());
            this._behaviour.RegisterBehaviour(new DGCraftingBehavior());
            this._behaviour.RegisterBehaviour(new DGResourceAcquisitionBehavior());
            this._behaviour.RegisterBehaviour(new DGItemAcquisitionBehavior());
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            this._behaviour.Act();
        }
    }
}