using DeadlyGame.Core.Components;
using DeadlyGame.Core.Entities;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGInformationsComponent : DGComponent
    {
        public byte Age { get; private set; }

        public DGInformationsComponent(DGGame game, DGEntity entity) : base(game, entity)
        {

        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
        }

        public void Randomize()
        {
            this.Age = (byte)this.DGGameInstance.RandomMath.Range(20, 60);
        }
    }
}
