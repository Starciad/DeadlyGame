using DeadlyGame.Core.Components.Common;
using DeadlyGame.Core.Enums.Personalities;

namespace DeadlyGame.Core.Information.Components
{
    public struct DGPersonalityInfo
    {
        public DGPersonalityType PersonalityType { get; set; }
        public DGCourageLevel CourageLevel { get; set; }

        public DGPersonalityInfo()
        {
            this.PersonalityType = DGPersonalityType.None;
            this.CourageLevel = DGCourageLevel.None;
        }

        internal static DGPersonalityInfo Create(DGPersonalityComponent component)
        {
            return new()
            {
                PersonalityType = component.PersonalityType,
                CourageLevel = component.CourageLevel
            };
        }
    }
}
