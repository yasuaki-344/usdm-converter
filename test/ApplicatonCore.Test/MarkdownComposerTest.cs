// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Services;
using Xunit;

namespace UsdmConverter.ApplicatonCore.Test
{
    public class MarkdownComposerTest
    {
        [Fact]
        public void ComposeCorrectly()
        {
            var expect = @"# title

## [REQ01]requirement1

### 理由

reason1

### 説明

description1

### [REQ01-01]requirement1-1

#### 理由

reason1-1

#### 説明

description1-1

#### 仕様

<group>

* [ ] [SPC01-01-01]description1
* [x] [SPC01-01-02]description2
";
            var data = new RequirementSpecification
            {
                Title = "title",
                Requirements = new List<UpperRequirement>()
                {
                    new UpperRequirement
                    {
                        ID = "REQ01",
                        Summary = "requirement1",
                        Reason = "reason1",
                        Description = "description1",
                        Requirements = new List<LowerRequirement>()
                        {
                            new LowerRequirement
                            {
                                ID = "REQ01-01",
                                Summary = "requirement1-1",
                                Reason = "reason1-1",
                                Description = "description1-1",
                                SpecificationGroups = new List<SpecificationGroup>()
                                {
                                    new SpecificationGroup
                                    {
                                        Category = "group",
                                        Specifications = new List<Specification>()
                                        {
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPC01-01-01",
                                                Description = "description1"
                                            },
                                            new Specification
                                            {
                                                IsImplemented = true,
                                                ID = "SPC01-01-02",
                                                Description = "description2"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
            };

            var target = new MarkdownComposer();
            var actual = target.Compose(data);

            Assert.Equal(expect, actual);
        }
    }
}
