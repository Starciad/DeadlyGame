﻿using DG.Core.Builders;
using DG.Core.Components.Common;
using DG.Core.Entities.Players;
using DG.Core.Objects;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DG.Core.Managers
{
    internal sealed class DGPlayerManager : DGObject
    {
        internal DGPlayer[] TotalPlayers => this.players;
        internal DGPlayer[] ActivePlayers => this.players.Where(x => x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead).ToArray();
        internal DGPlayer[] DisabledPlayers => this.players.Where(x => !x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead).ToArray();

        internal bool OnlyOneActivePlayer => this.ActivePlayers.Length > 0;

        private DGPlayer[] players;

        internal async Task InitializeAsync(IEnumerable<DGPlayerBuilder> playerBuilders)
        {
            DGPlayerBuilder[] playerBuildersArray = playerBuilders.ToArray();
            int length = playerBuildersArray.Length;

            this.players = new DGPlayer[length];

            for (int i = 0; i < length; i++)
            {
                this.players[i] = new(playerBuildersArray[i], (int)i);
                this.players[i].SetGameInstance(this.Game);
                this.players[i].Initialize();
            }

            await Task.CompletedTask;
        }

        internal async Task UpdateAsync()
        {
            await Task.CompletedTask;
        }
    }
}