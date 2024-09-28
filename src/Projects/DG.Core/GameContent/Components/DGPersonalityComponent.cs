using DeadlyGame.Core.Components;
using DeadlyGame.Core.Enums.Personalities;

namespace DeadlyGame.Core.GameContent.Components
{

    public sealed class DGPersonalityComponent : DGComponent
    {
        public DGPersonalityType PersonalityType { get; private set; }
        public DGCourageLevel CourageLevel { get; private set; }

        public void Randomize()
        {
            this.PersonalityType = (DGPersonalityType)this.Game.Random.Range(0, 6);
            this.CourageLevel = (DGCourageLevel)this.Game.Random.Range(0, 5);
        }
    }
}
