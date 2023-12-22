using DG.Core;
using DG.Core.Builders;

namespace DG
{
    internal static class Program
    {
        private static void Main()
        {
            DGGameBuilder gameBuilder = new()
            {
                Players = BuildPlayers(100)
            };

            DGWorldBuilder worldBuilder = new()
            {
                Size = new(100),
                Resources = new()
                {
                    TreeCount = 100,
                    StoneCount = 100,
                    ShrubCount = 100
                },
            };

            DGGame game = new(gameBuilder, worldBuilder);
            game.Initialize();
            game.Start();
        }

        private static DGPlayerBuilder[] BuildPlayers(int amount)
        {
            DGPlayerBuilder[] players = new DGPlayerBuilder[amount];

            for (int i = 0; i < amount; i++)
            {
                players[i] = new()
                {
                    Name = $"Player_{i}",
                };
            }

            return players;
        }
    }
}
