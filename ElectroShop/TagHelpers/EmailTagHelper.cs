using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string Address { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// Taghelper to send email to conntact ElectroShop, this is deisplayed at the ContactController
        /// </summary>
        /// <param name="context">The context of email</param>
        /// <param name="output">a mailto and adress and content of mail.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + Address);
            output.Content.SetContent(Content);
        }
    }
}
            