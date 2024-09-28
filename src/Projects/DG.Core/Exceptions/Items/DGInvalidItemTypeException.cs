using System;

namespace DeadlyGame.Core.Exceptions.Items
{
    public sealed class DGInvalidItemTypeException : Exception
    {
        public DGInvalidItemTypeException()
        {
        }

        public DGInvalidItemTypeException(string message)
            : base(message)
        {
        }

        public DGInvalidItemTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
