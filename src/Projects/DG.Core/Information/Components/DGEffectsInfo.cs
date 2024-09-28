using DeadlyGame.Core.Components.Common;
using DeadlyGame.Core.Effects;
using DeadlyGame.Core.Enums.Effects;

namespace DeadlyGame.Core.Information.Components
{
    public struct DGEffectsInfo
    {
        public DGEffectInfo[] Effects { get; set; }

        public DGEffectsInfo()
        {
            this.Effects = [];
        }

        internal static DGEffectsInfo Create(DGEffectsComponent component)
        {
            DGEffect[] effects = component.Effects;
            int length = effects.Length;

            DGEffectInfo[] effectsInfo = new DGEffectInfo[length];

            for (int i = 0; i < length; i++)
            {
                effectsInfo[i] = DGEffectInfo.Create(effects[i]);
            }

            return new()
            {
                Effects = effectsInfo,
            };
        }
    }

    public struct DGEffectInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DGEffectType EffectType { get; set; }
        public int Durability { get; set; }
        public int RemainingDurability { get; set; }
        public bool IsFinished { get; set; }

        public DGEffectInfo()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.EffectType = DGEffectType.None;
            this.Durability = 0;
            this.RemainingDurability = 0;
            this.IsFinished = false;
        }

        internal static DGEffectInfo Create(DGEffect effect)
        {
            return new()
            {
                Name = effect.Name,
                Description = effect.Description,
                EffectType = effect.EffectType,
                Durability = effect.Durability,
                RemainingDurability = effect.RemainingDurability,
                IsFinished = effect.IsFinished,
            };
        }
    }
}
