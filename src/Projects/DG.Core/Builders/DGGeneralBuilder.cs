namespace DeadlyGame.Core.Builders
{
    public struct DGGeneralBuilder
    {
        public required (string language, string region) LocalizationCode { get; set; }
        public int Seed { get; set; }

        public DGGeneralBuilder()
        {
            this.LocalizationCode = ("en", "US");
            this.Seed = -1;
        }
    }
}
