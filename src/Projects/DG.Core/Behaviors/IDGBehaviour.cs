using DeadlyGame.Core.Behaviors.Models;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Models.Infos.Actions;

namespace DeadlyGame.Core.Behaviors
{
    internal interface IDGBehaviour
    {
        bool CanAct(DGEntity entity, DGGame game);
        DGBehaviourWeight GetWeight();
        DGPlayerActionInfo Act();
    }
}
