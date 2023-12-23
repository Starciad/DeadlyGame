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
        // Managers
        internal DGPlayerManager PlayerManager => this._playersManager;
        internal DGWorldManager WorldManager => this._worldManager;
        internal DGRoundManager RoundManager => this._roundManager;

        // Utilities
        internal DGRandomUtilities Random { get; } = new();
        internal DGDice Dice { get; } = new();

        // Settings
        private readonly DGGameSettings _gameSettings = new(gameBuilder);

        // Managers
        private readonly DGPlayerManager _playersManager = new();
        private readonly DGWorldManager _worldManager = new();
        private readonly DGRoundManager _roundManager = new();

        public void Initialize()
        {
            this._worldManager.SetGameInstance(this);
            this._playersManager.SetGameInstance(this);
            this._roundManager.SetGameInstance(this);

            this._worldManager.Initialize(worldBuilder);
            this._playersManager.Initialize(this._gameSettings.Players);
        }
        public void Start()
        {
            while (!this._playersManager.OnlyOneActivePlayer)
            {

            }
        }
        public void Update()
        {
            // Round starts only when it is a new day.
            if (this._worldManager.CurrentDaylightCycle == DGWorldDaylightCycleState.Day)
            {
                this._roundManager.Begin();
            }

            // Update of all components.
            this._playersManager.Update();
            this._worldManager.Update();

            // Round ends only when it is one night.
            if (this._worldManager.CurrentDaylightCycle == DGWorldDaylightCycleState.Night)
            {
                this._roundManager.End();
            }
        }
    }
}