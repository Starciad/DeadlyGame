using System;

namespace DG.Core.Exceptions
{
    public class DGInvalidComponentTypeException : Exception
    {
        public DGInvalidComponentTypeException()
        {
        }

        public DGInvalidComponentTypeException(string message)
            : base(message)
        {
        }

        public DGInvalidComponentTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
