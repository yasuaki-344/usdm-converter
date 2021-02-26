using System.Collections.Generic;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Services;
using Xunit;
using Xunit.Sdk;

namespace UsdmConverter.ApplicatonCore.Test
{
    public class ExcelDecoderTest
    {
        [Fact]
        public void DecomposeHeadingCorrectly()
        {
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
                                ID = "REQ01",
                                Summary = "requirement1",
                                Reason = "reason1",
                                Description = "description1",
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

            var target = new ExcelDecoder();
            var book = target.Decode(data);
        }
    }
}
