﻿namespace DG.Core.Objects
{
    public class DGObject
    {
        protected DGGame Game { get; private set; }

        internal virtual void SetGameInstance(DGGame game)
        {
            this.Game = game;
        }
        public virtual void Initialize() { return; }
        public virtual void Update() { return; }
    }
}
