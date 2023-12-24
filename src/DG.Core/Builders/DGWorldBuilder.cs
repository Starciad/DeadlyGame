using System.Numerics;

namespace DG.Core.Builders
{
    public struct DGWorldBuilder
    {
        public required Vector2 Size { get; set; }
        public required DGWorldResourcesBuilder Resources { get; set; }
    }

    public struct DGWorldResourcesBuilder
    {
        public required int TreeCount { get; set; }
        public required int StoneCount { get; set; }
        public required int ShrubCount { get; set; }
    }
}
