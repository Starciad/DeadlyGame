using DG.Core.Components.Common;

namespace DG.Core.Information.Components
{
    public struct DGHungerInfo
    {
        public bool IsHungry { get; set; }
        public int CurrentHunger { get; set; }
        public int MaximumHunger { get; set; }

        public DGHungerInfo()
        {
            this.IsHungry = false;
            this.CurrentHunger = 0;
            this.MaximumHunger = 0;
        }

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
