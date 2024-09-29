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
        public required int TreeRate { get; set; }
        public required int StoneRate { get; set; }
        public required int ShrubRate { get; set; }
    }
}
