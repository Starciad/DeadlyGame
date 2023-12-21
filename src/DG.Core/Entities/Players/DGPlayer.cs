using DG.Core.Builders;

namespace DG.Core.Entities.Players
{
    internal sealed class DGPlayer : DGEntity
    {
        internal uint Index { get; private set; }

        internal DGPlayer(DGPlayerBuilder builder, uint index)
        {
            this.Name = builder.Name;
            this.Index = index;

            // Components
            // this.ComponentContainer.AddComponent<DGTransformComponent>();
            // this.ComponentContainer.AddComponent<DGGeneralComponent>();
            // this.ComponentContainer.AddComponent<DGHealthComponent>();
            // this.ComponentContainer.AddComponent<DGSanityComponent>();
            // this.ComponentContainer.AddComponent<DGCharacteristicsComponent>();
        }

        public override void Initialize()
        {

        }

        public override void Update()
        {

        }
    }
}
