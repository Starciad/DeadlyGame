namespace DG.Core.Behaviour.Models
{
    public struct DGBehaviourActInfos
    {
        public readonly string Title => this._title;
        public readonly string Description => this._description;

        private string _title;
        private string _description;

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
