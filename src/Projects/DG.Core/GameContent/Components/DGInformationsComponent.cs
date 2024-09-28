using DeadlyGame.Core.Components;

namespace DeadlyGame.Core.GameContent.Components
{
    internal sealed class DGInformationsComponent : DGComponent
    {
        internal byte Age { get; private set; }

        internal void Randomize()
        {
            this.Age = (byte)this.Game.Random.Range(20, 60);
        }
    }
}
