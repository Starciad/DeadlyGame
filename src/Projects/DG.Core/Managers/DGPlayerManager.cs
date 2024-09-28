using DeadlyGame.Core.Builders;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Entities.Players;
using DeadlyGame.Core.Mathematics.Primitives;

using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.Core.Managers
{
    public sealed class DGPlayerManager : DGManager
    {
        public uint TotalPlayerCount => (uint)this.players.Length;

        public DGPlayer[] Players => this.players;
        public DGPlayer[] LivingPlayers => [.. this.players.Where(x => !x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead)];
        public DGPlayer[] DeadPlayers => [.. this.players.Where(x => x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead)];

        public bool OnlyOnePlayerAlive => this.LivingPlayers.Length == 1;

        private DGPlayer[] players;

        public DGPlayerManager(DGGame game, IEnumerable<DGPlayerBuilder> playerBuilders) : base(game)
        {
            DGPlayerBuilder[] playerBuildersArray = playerBuilders.ToArray();
            int length = playerBuildersArray.Length;

            this.players = new DGPlayer[length];

            for (int i = 0; i < length; i++)
            {
                this.players[i] = new(this.DGGameInstance, playerBuildersArray[i], i);
            }
        }

        public override void Update()
        {
            foreach (DGPlayer player in this.LivingPlayers)
            {
                player.Update();
            }
        }

        public DGPlayer[] GetNearbyPlayers(DGPoint position)
        {
            return
            [
                .. this.players.OrderByDescending(x => DGPoint.Distance(x.ComponentContainer.GetComponent<DGTransformComponent>().Position, position)),
            ];
        }
    }
}