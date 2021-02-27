using System;

namespace UsdmConverter.ApplicationCore.Exceptions
{
    public class UsdmFormatException : Exception
    {
        public UsdmFormatException()
            : base()
        {
        }

        public UsdmFormatException(string message)
            : base(message)
        {
        }
    }
}
