using DG.Core.Behaviour.Common;
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

        public override void Initialize()
        {
            base.Initialize();

            // Components
            // Transform
            DGTransformComponent transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            transform.SetPosition(this.Game.WorldManager.GetRandomPosition());

            // Infos
            DGInformationsComponent informations = this.ComponentContainer.AddComponent<DGInformationsComponent>();
            informations.Randomize();

            // Personality
            DGPersonalityComponent personality = this.ComponentContainer.AddComponent<DGPersonalityComponent>();
            personality.Randomize();

            // Characteristics
            DGCharacteristicsComponent characteristics = this.ComponentContainer.AddComponent<DGCharacteristicsComponent>();
            characteristics.Randomize();

            // Health
            DGHealthComponent health = this.ComponentContainer.AddComponent<DGHealthComponent>();
            health.SetMaximumHealth(10 + DGAttributesUtilities.GetAttributeModifier(characteristics.Constitution));
            health.SetCurrentHealth(health.MaximumHealth);

            // Hunger
            DGHungerComponent hunger = this.ComponentContainer.AddComponent<DGHungerComponent>();
            hunger.SetMaximumHunger(100);
            hunger.SetCurrentHunger(0);

            // Inventory
            _ = this.ComponentContainer.AddComponent<DGInventoryComponent>();

            // Equipment
            _ = this.ComponentContainer.AddComponent<DGEquipmentComponent>();

            // Combat Infos
            DGCombatInfosComponent combatInfos = this.ComponentContainer.AddComponent<DGCombatInfosComponent>();
            combatInfos.SetInitiativeValue(DGAttributesUtilities.GetAttributeModifier(characteristics.Dexterity));
            combatInfos.SetDisplacementRateValue(9 + this.Game.Random.Range(-2, 3));

            // Relationship
            _ = this.ComponentContainer.AddComponent<DGRelationshipsComponent>();

            // Behaviour
            this._behaviour = this.ComponentContainer.AddComponent<DGBehaviourComponent>();
            this._behaviour.RegisterBehaviour(new DGMovementBehavior());
        }

        public override void Update()
        {
            base.Update();
            this._behaviour.Act();
        }
    }
}