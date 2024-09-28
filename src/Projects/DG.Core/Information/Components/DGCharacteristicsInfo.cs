using DG.Core.Components.Common;

namespace DG.Core.Information.Components
{
    public struct DGCharacteristicsInfo
    {
        public int Strength { get; set; }
        public int Constitution { get; set; }
        public int Dexterity { get; set; }
        public int Charisma { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }

        public DGCharacteristicsInfo()
        {
            this.Strength = 0;
            this.Constitution = 0;
            this.Dexterity = 0;
            this.Charisma = 0;
            this.Intelligence = 0;
            this.Wisdom = 0;
        }

        internal static DGCharacteristicsInfo Create(DGCharacteristicsComponent component)
        {
            return new()
            {
                Strength = component.Strength,
                Constitution = component.Constitution,
                Dexterity = component.Dexterity,
                Charisma = component.Charisma,
                Intelligence = component.Intelligence,
                Wisdom = component.Wisdom
            };
        }
    }
}
