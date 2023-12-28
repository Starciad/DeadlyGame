using DG.Core.Builders;
using DG.Settings;

using System;

namespace DG.Utilities
{
    internal static class PlayersUtilities
    {
        internal static DGPlayerBuilder[] CreatePlayers(DGIniReader settings)
        {
            int amount = int.Parse(settings.Read("Players", "Amount"));
            string[] firstNames = settings.Read("Players", "First_Names").Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] lastNames = settings.Read("Players", "Last_Names").Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            DGPlayerBuilder[] players = new DGPlayerBuilder[amount];
            for (int i = 0; i < amount; i++)
            {
                players[i] = new()
                {
                    Name = string.Concat(firstNames[RandomUtilities.Random.Next(0, firstNames.Length)], " ", lastNames[RandomUtilities.Random.Next(0, lastNames.Length)])
                };
            }

            return players;
        }
    }
}
