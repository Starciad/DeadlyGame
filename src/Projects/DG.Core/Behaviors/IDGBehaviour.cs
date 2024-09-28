namespace DeadlyGame.Core.Behaviors
{
    internal interface IDGBehaviour
    {
        bool CanAct(DGEntity entity, DGGame game);
        DGBehaviourWeight GetWeight();
        DGPlayerActionInfo Act();
    }
}
