using System;

namespace DG.Core.Exceptions.Components
{
    public sealed class DGDuplicateComponentsException : Exception
    {
        public DGDuplicateComponentsException()
        {
        }

        public DGDuplicateComponentsException(string message)
            : base(message)
        {
        }

        public DGDuplicateComponentsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
