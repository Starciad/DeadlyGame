using System;

namespace DeadlyGame.Core.Exceptions.Items
{
    internal sealed class DGInvalidItemTypeException : Exception
    {
        internal DGInvalidItemTypeException()
        {
        }

        internal DGInvalidItemTypeException(string message)
            : base(message)
        {
        }

        internal DGInvalidItemTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
