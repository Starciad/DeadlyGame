using DG.Core.Components.Common;

namespace DG.Core.Information.Components
{
    public struct DGHealthInfo
    {
        public bool IsDead { get; set; }
        public int CurrentHealth { get; set; }
        public int MaximumHealth { get; set; }

        public DGHealthInfo()
        {
            this.IsDead = false;
            this.CurrentHealth = 0;
            this.MaximumHealth = 0;
        }

        internal static DGHealthInfo Create(DGHealthComponent component)
        {
            return new()
            {
                IsDead = component.IsDead,
                CurrentHealth = component.CurrentHealth,
                MaximumHealth = component.MaximumHealth
            };
        }
    }
}
