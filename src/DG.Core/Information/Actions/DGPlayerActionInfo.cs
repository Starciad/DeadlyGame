namespace DG.Core.Information.Actions
{
    public struct DGPlayerActionInfo
    {
        public readonly bool IsEmpty => string.IsNullOrEmpty(this._title) && string.IsNullOrEmpty(this._description);
        public readonly string Title => this._title;
        public readonly string Description => this._description;

        private string _title;
        private string _description;

        public DGPlayerActionInfo()
        {
            this._title = string.Empty;
            this._description = string.Empty;
        }

        public void WithTitle(string value)
        {
            this._title = value;
        }

        public void WithDescription(string value)
        {
            this._description = value;
        }
    }
}
