using System;

namespace UsdmConverter.ApplicationCore.Exceptions
{
    public class UsdmMarkDownFormatException : Exception
    {
        public UsdmMarkDownFormatException()
            : base()
        {
        }

        public UsdmMarkDownFormatException(string message)
            : base(message)
        {
        }
    }
}
