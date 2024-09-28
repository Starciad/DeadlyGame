using System;
using System.Drawing;

namespace DG.Core.Information.Actions
{
    public struct DGPlayerActionInfo
    {
        public readonly bool IsEmpty => string.IsNullOrWhiteSpace(this._name) && string.IsNullOrWhiteSpace(this._title) && string.IsNullOrWhiteSpace(this._description) && this.PriorityLevel == -1 && this._authorId == -1 && this._involvedIds.Length == 0;

        public readonly string Name => this._name;
        public readonly string Title => this._title;
        public readonly string Description => this._description;
        public readonly int PriorityLevel => this._priorityLevel;
        public readonly int AuthorId => this._authorId;
        public readonly int[] InvolvedIds => this._involvedIds;
        public readonly Color Color => this._color;

        private string _name;
        private string _title;
        private string _description;
        private int _priorityLevel;
        private int _authorId;
        private int[] _involvedIds;
        private Color _color;

        public DGPlayerActionInfo()
        {
            this._name = string.Empty;
            this._title = string.Empty;
            this._description = string.Empty;
            this._priorityLevel = -1;
            this._authorId = -1;
            this._involvedIds = [];
            this._color = Color.White;
        }

        public void WithName(string name)
        {
            this._name = name;
        }
        public void WithTitle(string value)
        {
            this._title = value;
        }
        public void WithDescription(string value)
        {
            this._description = value;
        }
        public void WithPriorityLevel(int level)
        {
            this._priorityLevel = Math.Clamp(level, 0, 10);
        }
        public void WithAuthor(int id)
        {
            this._authorId = id;
        }
        public void WithInvolved(params int[] ids)
        {
            this._involvedIds = ids;
        }
        public void WithColor(Color color)
        {
            this._color = color;
        }
    }
}
