using DeadlyGame.Core.Builders;
using DeadlyGame.Core.Components.Common;
using DeadlyGame.Core.Entities.Players;
using DeadlyGame.Core.Information.Actions;
using DeadlyGame.Core.Information.Players;

using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DeadlyGame.Core.Managers
{
    internal sealed class DGPlayerManager : DGManager
    {
        internal DGPlayer[] TotalPlayers => this.players;
        internal DGPlayer[] ActivePlayers => this.players.Where(x => !x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead).ToArray();
        internal DGPlayer[] DisabledPlayers => this.players.Where(x => x.ComponentContainer.GetComponent<DGHealthComponent>().IsDead).ToArray();

        internal bool OnlyOneActivePlayer => this.ActivePlayers.Length == 1;

        private DGPlayer[] players;

        // System
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
        protected override void OnUpdate()
        {
            foreach (DGPlayer player in this.ActivePlayers)
            {
                player.Update();
            }
        }

        // Utilities
        internal DGPlayer[] GetNearbyPlayers(Vector2 position)
        {
            return
            [
                .. this.players.OrderByDescending(x => Vector2.Distance(x.ComponentContainer.GetComponent<DGTransformComponent>().Position, position)),
            ];
        }

        // Tools
        internal DGPlayerCollectionInfo GetPlayersInfo()
        {
            return new()
            {
                Players = GetInfo(),
            };

            // ===== METHODS =====
            DGPlayerInfo[] GetInfo()
            {
                int length = this.players.Length;
                DGPlayerInfo[] playersInfo = new DGPlayerInfo[length];
                for (int i = 0; i < length; i++)
                {
                    playersInfo[i] = DGPlayerInfo.Create(this.players[i]);
                }

                return playersInfo;
            }
        }
        internal DGPlayerActionCollectionInfo GetPlayersActionsInfo()
        {
            List<DGPlayerActionInfo> actionsFound = [];

            foreach (DGPlayer player in this.ActivePlayers)
            {
                if (player.ComponentContainer.TryGetComponent(out DGBehaviourComponent component))
                {
                    DGPlayerActionInfo actionInfo = component.LastActionInfos;
                    if (actionInfo.IsEmpty)
                    {
                        continue;
                    }

                    actionsFound.Add(actionInfo);
                }
            }

            return new()
            {
                Actions = [.. actionsFound],
            };
        }
    }
}