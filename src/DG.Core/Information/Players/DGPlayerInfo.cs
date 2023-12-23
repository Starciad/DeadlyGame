using DG.Core.Components.Common;
using DG.Core.Effects;
using DG.Core.Entities.Players;
using DG.Core.Information.Components;

namespace DG.Core.Information.Players
{
    public struct DGPlayerInfo
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public DGEffectInfo[] Effects { get; set; }

        public DGPlayerInfo(DGPlayer player)
        {
            this.Name = player.Name;
            this.Index = player.Index;

            GetAllEffects(player);
        }

        private void GetAllEffects(DGPlayer player)
        {
            if (player.ComponentContainer.TryGetComponent(out DGEffectsComponent effectsComponent))
            {
                DGEffect[] effects = effectsComponent.Effects;
                int length = effects.Length;

                this.Effects = new DGEffectInfo[length];

                for (int i = 0; i < length; i++)
                {
                    this.Effects[i] = new(effects[i]);
                }
            }
        }
    }
}
