// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

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
