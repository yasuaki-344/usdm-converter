﻿// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
using NPOI.SS.UserModel;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Interfaces;

namespace UsdmConverter.ApplicationCore.Services
{
    public class ExcelEncoder : IExcelEncoder
    {
        /// <summary>
        /// Initializes a new instance of ExcelEncoder class.
        /// </summary>
        public ExcelEncoder()
        {
        }

        public RequirementSpecification Encode(IWorkbook book)
        {
            throw new System.NotImplementedException();
        }
    }
}
