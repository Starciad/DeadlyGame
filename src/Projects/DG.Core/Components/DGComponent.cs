﻿namespace DeadlyGame.Core.Components
{
    internal class DGComponent : DGObject
    {
        protected DGEntity Entity { get; private set; }

        internal void SetEntityInstance(DGEntity entity)
        {
            this.Entity = entity;
        }
    }
}
