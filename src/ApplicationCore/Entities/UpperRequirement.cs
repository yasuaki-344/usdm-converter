// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;

namespace UsdmConverter.ApplicationCore.Entities
{
    /// <summary>
    /// Upper requirement entity
    /// </summary>
    public class UpperRequirement
    {
        public string ID { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<LowerRequirement> Requirements { get; set; } = new List<LowerRequirement>();
    }
}
