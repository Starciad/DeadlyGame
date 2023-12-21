namespace DG.Core.Objects
{
    public class DGObject
    {
        internal bool IsActive { get; set; }

        internal virtual void Initialize() { }
        internal virtual void Update() { }
    }
}
