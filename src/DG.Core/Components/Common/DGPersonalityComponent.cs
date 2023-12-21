namespace DG.Core.Components.Common
{
    public enum DGPersonalityType
    {
        Aggressive = 0,
        Cautious = 1,
        Impulsive = 2,
        Strategic = 3,
        Explorer = 4,
    }
    public enum DGCourageLevel
    {
        Fearless = 0,
        Brave = 1,
        Cautious = 2,
        Fearful = 3
    }

    internal sealed class DGPersonalityComponent : DGComponent
    {
        internal DGPersonalityType PersonalityType { get; private set; }
        internal DGCourageLevel CourageLevel { get; private set; }

        internal void Randomize()
        {
            PersonalityType = (DGPersonalityType)this.Game.Random.Range(0, 6);
            CourageLevel = (DGCourageLevel)this.Game.Random.Range(0, 5);
        }
    }
}
