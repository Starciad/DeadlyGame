using DG.Core.Behaviour.Common;
using DG.Core.Builders;
using DG.Core.Components;
using DG.Core.Components.Common;
using DG.Core.Utilities;

namespace DG.Core.Entities.Players
{
    internal sealed class DGPlayer : DGEntity
    {
        internal int Index { get; private set; }

        // Components
        private DGTransformComponent _transform;
        private DGInformationsComponent _informations;
        private DGHealthComponent _health;
        private DGHungerComponent _hunger;
        private DGCharacteristicsComponent _characteristics;
        private DGInventoryComponent _inventory;
        private DGEquipmentComponent _equipment;
        private DGCombatInfosComponent _action;
        private DGBehaviourComponent _behaviour;
        private DGPersonalityComponent _personality;

        internal DGPlayer(DGPlayerBuilder builder, int index)
        {
            this.Name = builder.Name;
            this.Index = index;
        }

        public override void Initialize()
        {
            base.Initialize();

            // Components
            this._transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            this._informations = this.ComponentContainer.AddComponent<DGInformationsComponent>();
            this._health = this.ComponentContainer.AddComponent<DGHealthComponent>();
            this._hunger = this.ComponentContainer.AddComponent<DGHungerComponent>();
            this._characteristics = this.ComponentContainer.AddComponent<DGCharacteristicsComponent>();
            this._inventory = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            this._equipment = this.ComponentContainer.AddComponent<DGEquipmentComponent>();
            this._action = this.ComponentContainer.AddComponent<DGCombatInfosComponent>();
            this._behaviour = this.ComponentContainer.AddComponent<DGBehaviourComponent>();
            this._personality = this.ComponentContainer.AddComponent<DGPersonalityComponent>();

            // transform
            this._transform.SetPosition(this.Game.WorldManager.GetRandomPosition());

            // infos
            this._informations.Randomize();

            // health
            this._health.SetMaximumHealth(10 + DGAttributesUtilities.GetAttributeModifier(this._characteristics.Constitution));
            this._health.SetCurrentHealth(this._health.MaximumHealth);

            // hunger
            this._hunger.SetMaximumHunger(100);
            this._hunger.SetCurrentHunger(0);

            // characteristics
            this._characteristics.Randomize();

            // action
            this._action.SetInitiativeValue(DGAttributesUtilities.GetAttributeModifier(this._characteristics.Dexterity));
            this._action.SetDisplacementRateValue(9 + this.Game.Random.Range(-2, 3));

            // behaviour
            this._behaviour.RegisterBehaviour(new DGMovementBehaviour());

            // personality
            this._personality.Randomize();
        }

        public override void Update()
        {
            base.Update();
            this._behaviour.Act();
        }
    }
}
