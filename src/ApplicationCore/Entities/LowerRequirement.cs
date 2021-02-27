// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;

namespace UsdmConverter.ApplicationCore.Entities
{
    /// <summary>
    /// Lower requirement entity
    /// </summary>
    public class LowerRequirement
    {
        public string ID { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<SpecificationGroup> SpecificationGroups { get; set; } = new List<SpecificationGroup>();
    }
}
