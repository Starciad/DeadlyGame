using DG.Core.Behaviour.Models;
using DG.Core.Entities;
using DG.Core.Information.Actions;

namespace DG.Core.Behaviour
{
    internal interface IDGBehaviour
    {
        bool CanAct(DGEntity entity, DGGame game);
        DGBehaviourWeight GetWeight();
        DGPlayerActionInfo Act();
    }
}
