using DG.Core.Behaviors.Models;
using DG.Core.Entities;
using DG.Core.Information.Actions;

namespace DG.Core.Behaviors
{
    internal interface IDGBehaviour
    {
        bool CanAct(DGEntity entity, DGGame game);
        DGBehaviourWeight GetWeight();
        DGPlayerActionInfo Act();
    }
}
