using System;

namespace DG.Core.Exceptions.Components
{
    internal sealed class DGDuplicateComponentsException : Exception
    {
        internal DGDuplicateComponentsException()
        {
        }

        internal DGDuplicateComponentsException(string message)
            : base(message)
        {
        }

        internal DGDuplicateComponentsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
