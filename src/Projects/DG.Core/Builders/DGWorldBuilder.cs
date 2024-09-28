using DeadlyGame.Core.Mathematics.Primitives;

namespace DeadlyGame.Core.Builders
{
    public struct DGWorldBuilder
    {
        public required DGPoint Size { get; set; }
        public required DGWorldResourcesBuilder Resources { get; set; }
    }

    public struct DGWorldResourcesBuilder
    {
        public required int TreeCount { get; set; }
        public required int StoneCount { get; set; }
        public required int ShrubCount { get; set; }
    }
}
