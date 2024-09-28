namespace DeadlyGame.Core.Information.World
{
    public struct DGWorldResourcesInfo
    {
        public DGWorldResourceInfo[] Resources { get; set; }
        public DGWorldItemInfo[] Items { get; set; }

        public DGWorldResourcesInfo()
        {
            this.Resources = [];
            this.Items = [];
        }
    }
}
