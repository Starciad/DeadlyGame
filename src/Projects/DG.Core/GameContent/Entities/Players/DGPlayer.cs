using DeadlyGame.Core.Builders;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Enums.Characters;
using DeadlyGame.Core.GameContent.Behaviors;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Effects;
using DeadlyGame.Core.Mathematics;

namespace DeadlyGame.Core.GameContent.Entities.Players
{
    public sealed class DGPlayer : DGEntity
    {
        public DGBiologicalSexType BiologicalSex => this.biologicalSex;

        private readonly DGBiologicalSexType biologicalSex;

        private readonly DGBehaviourComponent _behaviourComponent;
        private readonly DGTransformComponent _transformComponent;
        private readonly DGInformationsComponent _informationsComponent;
        private readonly DGPersonalityComponent _personalityComponent;
        private readonly DGCharacteristicsComponent _characteristicsComponent;
        private readonly DGHealthComponent _healthComponent;
        private readonly DGHungerComponent _hungerComponent;
        private readonly DGCombatComponent _combatInfosComponent;
        private readonly DGEffectsComponent _effectsComponent;

        public DGPlayer(DGGame game, DGPlayerBuilder builder, int id) : base(game)
        {
            this.Name = builder.Name;
            this.Id = id;
            this.biologicalSex = builder.BiologicalSex;

            this._transformComponent = this.ComponentContainer.AddComponent<DGTransformComponent>();
            this._informationsComponent = this.ComponentContainer.AddComponent<DGInformationsComponent>();
            this._personalityComponent = this.ComponentContainer.AddComponent<DGPersonalityComponent>();
            this._characteristicsComponent = this.ComponentContainer.AddComponent<DGCharacteristicsComponent>();
            this._healthComponent = this.ComponentContainer.AddComponent<DGHealthComponent>();
            this._hungerComponent = this.ComponentContainer.AddComponent<DGHungerComponent>();
            this._combatInfosComponent = this.ComponentContainer.AddComponent<DGCombatComponent>();
            this._effectsComponent = this.ComponentContainer.AddComponent<DGEffectsComponent>();
            this._behaviourComponent = this.ComponentContainer.AddComponent<DGBehaviourComponent>();

            _ = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            _ = this.ComponentContainer.AddComponent<DGEquipmentComponent>();
            _ = this.ComponentContainer.AddComponent<DGRelationshipsComponent>();
        }

        public override void Start()
        {
            this._transformComponent.SetPosition(this.DGGameInstance.WorldManager.GetRandomPosition());
            this._informationsComponent.Randomize();
            this._personalityComponent.Randomize();
            this._characteristicsComponent.Randomize();
            this._healthComponent.SetMaximumHealth(10 + DGAttributesMath.GetAttributeModifier(this._characteristicsComponent.Constitution));
            this._healthComponent.SetCurrentHealth(this._healthComponent.MaximumHealth);
            this._hungerComponent.SetMaximumHunger(100);
            this._hungerComponent.SetCurrentHunger(0);
            this._combatInfosComponent.SetDisplacementRateValue(9 + this.DGGameInstance.RandomMath.Range(-2, 3));
            this._behaviourComponent.RegisterBehaviour(new DGMovementBehavior());
            this._behaviourComponent.RegisterBehaviour(new DGAggressiveBehavior());
            this._behaviourComponent.RegisterBehaviour(new DGCraftingBehavior());
            this._behaviourComponent.RegisterBehaviour(new DGResourceAcquisitionBehavior());
            this._behaviourComponent.RegisterBehaviour(new DGItemAcquisitionBehavior());
            this._behaviourComponent.RegisterBehaviour(new DGSelfPreservationBehavior());
            this._behaviourComponent.RegisterBehaviour(new DGEquipmentBehavior());
        }

        public override void Update()
        {
            // If the player has paralyzing effects, he will not act.
            if (this._effectsComponent.HasEffect<DGRestEffect>())
            {
                return;
            }

            this._behaviourComponent.Act();
        }
    }
}