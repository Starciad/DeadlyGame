using DG.Core.Components.Common;

namespace DG.Core.Information.Components
{
    public struct DGInformationsInfo
    {
        public byte Age { get; set; }

        internal static DGInformationsInfo Create(DGInformationsComponent component)
        {
            return new()
            {
                Age = component.Age,
            };
        }
    }
}
