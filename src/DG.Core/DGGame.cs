using DG.Core.Builders;
using DG.Core.Dice;
using DG.Core.Managers;
using DG.Core.Settings;
using DG.Core.Utilities;

using System;

namespace DG.Core
{
    public sealed class DGGame(DGGameBuilder gameBuilder, DGWorldBuilder worldBuilder)
    {
        internal DGPlayerManager PlayerManager => this._playersManager;
        internal DGWorldManager WorldManager => this._worldManager;
        internal DGRandomUtilities Random { get; } = new();
        internal DGDice Dice { get; } = new();

        // Settings
        private readonly DGGameSettings _gameSettings = new(gameBuilder);

        // Managers
        private readonly DGPlayerManager _playersManager = new();
        private readonly DGWorldManager _worldManager = new();

        public void Initialize()
        {
            this._worldManager.SetGameInstance(this);
            this._playersManager.SetGameInstance(this);

            this._worldManager.Initialize(worldBuilder);
            this._playersManager.Initialize(this._gameSettings.Players);
        }

        public void Start()
        {
            int loop = 1;

            while (!this._playersManager.OnlyOneActivePlayer)
            {
                Console.WriteLine($"[ Loop: {loop} || Day ({this._worldManager.CurrentDay}): {this._worldManager.CurrentDaylightCycle} || Players: {this.PlayerManager.ActivePlayers.Length} ]");

                this._playersManager.Update();
                this._worldManager.Update();

                loop++;
            }

            Console.WriteLine("Finished");
        }
    }
}
