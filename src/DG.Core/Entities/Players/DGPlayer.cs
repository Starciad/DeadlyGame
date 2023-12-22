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

            DGTransformComponent transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            DGInformationsComponent informations = this.ComponentContainer.AddComponent<DGInformationsComponent>();
            DGPersonalityComponent personality = this.ComponentContainer.AddComponent<DGPersonalityComponent>();
            DGCharacteristicsComponent characteristics = this.ComponentContainer.AddComponent<DGCharacteristicsComponent>();
            DGHealthComponent health = this.ComponentContainer.AddComponent<DGHealthComponent>();
            DGHungerComponent hunger = this.ComponentContainer.AddComponent<DGHungerComponent>();
            DGCombatComponent combatInfos = this.ComponentContainer.AddComponent<DGCombatComponent>();

            _ = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            _ = this.ComponentContainer.AddComponent<DGEquipmentComponent>();
            _ = this.ComponentContainer.AddComponent<DGRelationshipsComponent>();

            // Components
            // Transform
            transform.SetPosition(this.Game.WorldManager.GetRandomPosition());

            // Infos
            informations.Randomize();

            // Personality
            personality.Randomize();

            // Characteristics
            characteristics.Randomize();

            // Health
            health.SetMaximumHealth(10 + DGAttributesUtilities.GetAttributeModifier(characteristics.Constitution));
            health.SetCurrentHealth(health.MaximumHealth);

            // Hunger
            hunger.SetMaximumHunger(100);
            hunger.SetCurrentHunger(0);

            // Combat
            combatInfos.SetDisplacementRateValue(9 + this.Game.Random.Range(-2, 3));

            // Behaviour
            this._behaviour = this.ComponentContainer.AddComponent<DGBehaviourComponent>();
            this._behaviour.RegisterBehaviour(new DGMovementBehavior());
            this._behaviour.RegisterBehaviour(new DGAggressiveBehavior());
            this._behaviour.RegisterBehaviour(new DGCraftingBehavior());
            this._behaviour.RegisterBehaviour(new DGResourceAcquisitionBehavior());
        }

        public override void Update()
        {
            base.Update();
            this._behaviour.Act();
        }
    }
}