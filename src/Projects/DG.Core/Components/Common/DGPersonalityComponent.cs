using DeadlyGame.Core.Enums.Personalities;

namespace DeadlyGame.Core.Components.Common
{

    internal sealed class DGPersonalityComponent : DGComponent
    {
        internal DGPersonalityType PersonalityType { get; private set; }
        internal DGCourageLevel CourageLevel { get; private set; }

        internal void Randomize()
        {
            this.PersonalityType = (DGPersonalityType)this.Game.Random.Range(0, 6);
            this.CourageLevel = (DGCourageLevel)this.Game.Random.Range(0, 5);
        }
    }
}
