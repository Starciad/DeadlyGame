using DG.Core.Components.Common;
using DG.Core.Effects;

namespace DG.Core.Information.Components
{
    public struct DGEffectsInfo
    {
        public DGEffectInfo[] Effects { get; set; }

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
