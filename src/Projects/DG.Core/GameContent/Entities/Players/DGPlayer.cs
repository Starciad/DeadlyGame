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

        private readonly DGBehaviourComponent _behaviour;
        private readonly DGTransformComponent _transform;
        private readonly DGInformationsComponent _informations;
        private readonly DGPersonalityComponent _personality;
        private readonly DGCharacteristicsComponent _characteristics;
        private readonly DGHealthComponent _health;
        private readonly DGHungerComponent _hunger;
        private readonly DGCombatComponent _combatInfos;
        private readonly DGEffectsComponent _effects;

        public DGPlayer(DGGame game, DGPlayerBuilder builder, int id) : base(game)
        {
            this.Name = builder.Name;
            this.Id = id;
            this.biologicalSex = builder.BiologicalSex;

            this._transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            this._informations = this.ComponentContainer.AddComponent<DGInformationsComponent>();
            this._personality = this.ComponentContainer.AddComponent<DGPersonalityComponent>();
            this._characteristics = this.ComponentContainer.AddComponent<DGCharacteristicsComponent>();
            this._health = this.ComponentContainer.AddComponent<DGHealthComponent>();
            this._hunger = this.ComponentContainer.AddComponent<DGHungerComponent>();
            this._combatInfos = this.ComponentContainer.AddComponent<DGCombatComponent>();
            this._effects = this.ComponentContainer.AddComponent<DGEffectsComponent>();
            this._behaviour = this.ComponentContainer.AddComponent<DGBehaviourComponent>();

            _ = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            _ = this.ComponentContainer.AddComponent<DGEquipmentComponent>();
            _ = this.ComponentContainer.AddComponent<DGRelationshipsComponent>();
        }

        public override void Start()
        {
            this._transform.SetPosition(this.DGGameInstance.WorldManager.GetRandomPosition());
            this._informations.Randomize();
            this._personality.Randomize();
            this._characteristics.Randomize();
            this._health.SetMaximumHealth(10 + DGAttributesMath.GetAttributeModifier(this._characteristics.Constitution));
            this._health.SetCurrentHealth(this._health.MaximumHealth);
            this._hunger.SetMaximumHunger(100);
            this._hunger.SetCurrentHunger(0);
            this._combatInfos.SetDisplacementRateValue(9 + this.DGGameInstance.RandomMath.Range(-2, 3));
            this._behaviour.RegisterBehaviour(new DGMovementBehavior());
            this._behaviour.RegisterBehaviour(new DGAggressiveBehavior());
            this._behaviour.RegisterBehaviour(new DGCraftingBehavior());
            this._behaviour.RegisterBehaviour(new DGResourceAcquisitionBehavior());
            this._behaviour.RegisterBehaviour(new DGItemAcquisitionBehavior());
            this._behaviour.RegisterBehaviour(new DGSelfPreservationBehavior());
            this._behaviour.RegisterBehaviour(new DGEquipmentBehavior());
        }

        public override void Update()
        {
            // If the player has paralyzing effects, he will not act.
            if (this._effects.HasEffect<DGRestEffect>())
            {
                return;
            }

            this._behaviour.Act();
        }
    }
}