using System.Collections.Generic;

namespace DG.Core.Builders
{
    public struct DGGameBuilder
    {
        public required IEnumerable<DGPlayerBuilder> Players { get; set; }
    }
}
