using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Managers
{
    public abstract class DGManager : DGGameObject
    {
        protected DGManager(DGGame game) : base(game)
        {

        }
    }
}
