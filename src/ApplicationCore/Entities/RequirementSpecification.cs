// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;

namespace UsdmConverter.ApplicationCore.Entities
{
    /// <summary>
    /// Requirement specification entity
    /// </summary>
    public class RequirementSpecification
    {
        public string Title { get; set; } = string.Empty;
        public List<UpperRequirement> Requirements { get; set; } = new List<UpperRequirement>();
    }
}
