namespace DeadlyGame.Core.Items.Types
{
    public abstract class DGFood : DGItem
    {
        public int SatietyFactor { get; set; }

        protected DGFood(DGGame game) : base(game)
        {

        }
    }
}
