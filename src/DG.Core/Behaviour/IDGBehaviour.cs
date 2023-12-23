using DG.Core.Behaviour.Models;
using DG.Core.Entities;
using DG.Core.Information.Actions;

namespace DG.Core.Behaviour
{
    internal interface IDGBehaviour
    {
        DGBehaviourWeight GetWeight(DGEntity entity, DGGame game);
        DGPlayerActionInfo Act(DGEntity entity, DGGame game);
    }
}
