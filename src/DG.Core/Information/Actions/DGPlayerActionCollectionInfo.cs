﻿namespace DG.Core.Information.Actions
{
    public struct DGPlayerActionCollectionInfo
    {
        public DGPlayerActionInfo[] Actions { get; set; }

        public DGPlayerActionCollectionInfo()
        {
            this.Actions = [];
        }
    }
}
