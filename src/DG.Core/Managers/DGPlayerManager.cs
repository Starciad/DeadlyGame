using DG.Core.Builders;
using DG.Core.Components.Common;
using DG.Core.Entities.Players;
using DG.Core.Objects;

using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DG.Core.Managers
{
    internal sealed class DGPlayerManager : DGObject
    {
        internal DGPlayer[] TotalPlayers => this.players;
        internal DGPlayer[] ActivePlayers => this.players.Where(x => !x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead).ToArray();
        internal DGPlayer[] DisabledPlayers => this.players.Where(x => x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead).ToArray();

        internal bool OnlyOneActivePlayer => this.ActivePlayers.Length == 1;

        private DGPlayer[] players;

        public void Initialize(IEnumerable<DGPlayerBuilder> playerBuilders)
        {
            DGPlayerBuilder[] playerBuildersArray = playerBuilders.ToArray();
            int length = playerBuildersArray.Length;

            this.players = new DGPlayer[length];

            for (int i = 0; i < length; i++)
            {
                this.players[i] = new(playerBuildersArray[i], i);
                this.players[i].SetGameInstance(this.Game);
                this.players[i].Initialize();
            }
        }

        public override void Update()
        {
            foreach (DGPlayer player in this.ActivePlayers)
            {
                player.Update();
            }
        }

        internal DGPlayer[] GetPlayersNearAnotherPlayer(DGPlayer target)
        {
            return
            [
                .. GetNearbyPlayersFromALocation(target.ComponentContainer.GetComponent<DGTransformComponent>().Position).Where(x => x != target)
            ];
        }

        internal DGPlayer[] GetNearbyPlayersFromALocation(Vector2 position)
        {
            return
            [
                .. this.players.OrderByDescending(x => Vector2.Distance(x.ComponentContainer.GetComponent<DGTransformComponent>().Position, position)),
            ];
        }
    }
}