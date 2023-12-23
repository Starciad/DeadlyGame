using DG.Core.Components.Common;

namespace DG.Core.Information.Components
{
    public struct DGHungerInfo
    {
        public bool IsHungry { get; set; }
        public int CurrentHunger { get; set; }
        public int MaximumHunger { get; set; }

        internal static DGHungerInfo Create(DGHungerComponent component)
        {
            return new()
            {
                IsHungry = component.IsHungry,
                CurrentHunger = component.CurrentHunger,
                MaximumHunger = component.MaximumHunger,
            };
        }
    }
}
