﻿// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
using NPOI.SS.UserModel;

namespace UsdmConverter.ApplicationCore.Interfaces
{
    public interface IExcelReader
    {
        IWorkbook Read(string filePath);
    }
}
