using DeadlyGame.Core.Components;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Enums.Personalities;

namespace DeadlyGame.Core.GameContent.Components
{

    public sealed class DGPersonalityComponent : DGComponent
    {
        public DGPersonalityType PersonalityType { get; private set; }
        public DGCourageLevel CourageLevel { get; private set; }

        public DGPersonalityComponent(DGGame game, DGEntity entity) : base(game, entity)
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
            this.PersonalityType = (DGPersonalityType)this.DGGameInstance.RandomMath.Range(0, 6);
            this.CourageLevel = (DGCourageLevel)this.DGGameInstance.RandomMath.Range(0, 5);
        }
    }
}
