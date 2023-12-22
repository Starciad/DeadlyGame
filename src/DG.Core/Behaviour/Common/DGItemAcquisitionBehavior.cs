using DG.Core.Behaviour.Models;
using DG.Core.Entities;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGItemAcquisitionBehavior : IDGBehaviour
    {
        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();



            return weight;
        }

        public DGBehaviourActInfos Act(DGEntity entity, DGGame game)
        {
            DGBehaviourActInfos infos = new();

            return infos;
        }
    }
}
