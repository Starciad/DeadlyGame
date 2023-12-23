using DG.Core.Components.Common;

using System.ComponentModel;

namespace DG.Core.Information.Components
{
    public struct DGPersonalityInfo
    {
        public DGPersonalityType PersonalityType { get; set; }
        public DGCourageLevel CourageLevel { get; set; }

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
