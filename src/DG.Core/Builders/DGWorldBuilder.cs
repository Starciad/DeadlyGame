using System.Numerics;

namespace DG.Core.Builders
{
    public struct DGWorldBuilder
    {
        public Vector2 Size { get; set; }
        public DGWorldResourcesBuilder Resources { get; set; }
    }

    public struct DGWorldResourcesBuilder
    {
        public int TreeCount { get; set; }
        public int StoneCount { get; set; }
        public int ShrubCount { get; set; }
    }
}
