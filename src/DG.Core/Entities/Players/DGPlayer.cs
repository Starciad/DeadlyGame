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
        private readonly DGTransformComponent _transform;
        private readonly DGInformationsComponent _informations;
        private readonly DGHealthComponent _health;
        private readonly DGCharacteristicsComponent _characteristics;
        private readonly DGInventoryComponent _inventory;
        private readonly DGEquipmentComponent _equipment;
        private readonly DGCombatComponent _action;

        internal DGPlayer(DGPlayerBuilder builder, int index)
        {
            this.Name = builder.Name;
            this.Index = index;

            // Components
            this._transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            this._informations = this.ComponentContainer.AddComponent<DGInformationsComponent>();
            this._health = this.ComponentContainer.AddComponent<DGHealthComponent>();
            this._characteristics = this.ComponentContainer.AddComponent<DGCharacteristicsComponent>();
            this._inventory = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            this._equipment = this.ComponentContainer.AddComponent<DGEquipmentComponent>();
            this._action = this.ComponentContainer.AddComponent<DGCombatComponent>();
        }

        public override void Initialize()
        {
            base.Initialize();

            // transform
            this._transform.SetPosition(this.Game.WorldManager.GetRandomPosition());

            // infos
            this._informations.Randomize();

            // health
            this._health.SetMaximumHealth(10 + DGAttributesUtilities.GetAttributeModifier(this._characteristics.Constitution));
            this._health.SetCurrentHealth(this._health.MaximumHealth);

            // characteristics
            this._characteristics.Randomize();

            // action
            this._action.SetInitiativeValue(DGAttributesUtilities.GetAttributeModifier(this._characteristics.Dexterity));
            this._action.SetDisplacementRateValue(9 + this.Game.Random.Range(-2, 3));
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
