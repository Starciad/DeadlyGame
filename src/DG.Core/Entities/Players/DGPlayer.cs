using DG.Core.Builders;
using DG.Core.Components.Common;

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
            AddComponent<DGTransformComponent>();
            AddComponent<DGGeneralComponent>();
            AddComponent<DGHealthComponent>();
            AddComponent<DGSanityComponent>();
            AddComponent<DGCharacteristicsComponent>();
            AddComponent<DGAttributesComponent>();
        }

        internal override void Initialize()
        {

        }

        internal override void Update()
        {

        }
    }
}
