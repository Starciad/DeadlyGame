using DG.Core.Effects;

namespace DG.Core.Information.Components
{
    public struct DGEffectInfo(DGEffect effect)
    {
        public string Name { get; set; } = effect.Name;
        public string Description { get; set; } = effect.Description;
        public DGEffectType EffectType { get; set; } = effect.EffectType;
        public int Durability { get; set; } = effect.Durability;
        public int RemainingDurability { get; set; } = effect.RemainingDurability;
        public bool IsFinished { get; set; } = effect.IsFinished;
    }
}
