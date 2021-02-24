using System;
using System.Linq;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Exceptions;
using UsdmConverter.ApplicationCore.Interfaces;

namespace UsdmConverter.ApplicationCore.Services
{
    public class MarkdownParser : IMarkdownParser
    {
        private readonly MarkdownDocument _document = new MarkdownDocument();

        /// <summary>
        /// Initializes a new instance of MarkdownParser class.
        /// </summary>
        public MarkdownParser()
        {
        }

        /// <summary>
        /// Parse a markdown document
        /// </summary>
        /// <param name="content">The markdown text</param>
        /// <returns></returns>
        public RequirementSpecification Parse(string content)
        {
            throw new NotImplementedException();
        }
    }
}
