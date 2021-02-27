// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using UsdmConverter.ApplicationCore.Entities;

namespace UsdmConverter.ApplicationCore.Interfaces
{
    public interface IMarkdownComposer
    {
        string Compose(RequirementSpecification data);
    }
}
