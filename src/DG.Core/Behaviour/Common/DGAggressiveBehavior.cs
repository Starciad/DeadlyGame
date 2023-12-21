using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Entities;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGAggressiveBehavior : IDGBehaviour
    {
        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();

            // === ADDITIONS ===
            // Personality [+2]

            // === SUBTRACTIONS ===

            return weight;
        }

        public DGBehaviourActInfos Act(DGEntity entity, DGGame game)
        {
            // Infos
            DGBehaviourActInfos infos = new();
            infos.WithTitle(string.Empty);
            infos.WithDescription(string.Empty);
            return infos;
        }
    }
}
