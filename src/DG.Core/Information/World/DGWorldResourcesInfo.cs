using System.Collections.Generic;

namespace DG.Core.Information.World
{
    public struct DGWorldResourcesInfo
    {
        public IEnumerable<DGWorldResourceInfo> Resources { get; set; }
        public IEnumerable<DGWorldItemInfo> Items { get; set; }
    }
}
