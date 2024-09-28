using DeadlyGame.Core.Components;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGInformationsComponent : DGComponent
    {
        public byte Age { get; private set; }

        public void Randomize()
        {
            this.Age = (byte)this.Game.Random.Range(20, 60);
        }
    }
}
