using System;

namespace ServiceLayer.ErrorUtils
{
    public class InvalidDeleteException : Exception
    {
        public InvalidDeleteException(string message)
            : base(message)
        {
        }
    }
}
