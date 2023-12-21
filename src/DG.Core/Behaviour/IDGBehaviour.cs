using DG.Core.Behaviour.Models;
using DG.Core.Entities;

namespace DG.Core.Behaviour
{
    internal interface IDGBehaviour
    {
        DGBehaviourWeight GetWeight(DGEntity entity, DGGame game);
        DGBehaviourActInfos Act(DGEntity entity, DGGame game);
    }
}
