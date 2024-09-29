using DeadlyGame.Core.Enums.Characters;

namespace DeadlyGame.Core.Builders
{
    public struct DGPlayerBuilder
    {
        public required string Name { get; set; }
        public required DGSexType Sex { get; set; }
    }
}