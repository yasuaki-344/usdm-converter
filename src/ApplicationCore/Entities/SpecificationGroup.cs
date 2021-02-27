// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;

namespace UsdmConverter.ApplicationCore.Entities
{
    /// <summary>
    /// Specification group entity
    /// </summary>
    public class SpecificationGroup
    {
        public string Category { get; set; } = string.Empty;
        public List<Specification> Specifications { get; set; } = new List<Specification>();
    }
}
