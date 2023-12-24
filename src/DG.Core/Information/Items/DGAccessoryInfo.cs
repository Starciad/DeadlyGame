using DG.Core.Items.Accessories;

namespace DG.Core.Information.Items
{
    public struct DGAccessoryInfo
    {
        public DGItemInfo Info { get; set; }

        public DGAccessoryInfo()
        {
            this.Info = new();
        }

        internal static DGAccessoryInfo Create(DGAccessory accessory)
        {
            return new()
            {
                Info = DGItemInfo.Create(accessory),
            };
        }
    }
}
