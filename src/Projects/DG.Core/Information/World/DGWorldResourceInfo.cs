namespace DeadlyGame.Core.Information.World
{
    public struct DGWorldResourceInfo
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public DGWorldResourceInfo()
        {
            this.Name = string.Empty;
            this.Count = 0;
        }
    }
}
