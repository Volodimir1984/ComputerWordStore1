using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComputerWordStore.TagHelpers
{
    // Tag helper for to render the form to select count products on the page.
    public class CountProductsOrSortTagHelper : TagHelper
    {
        public IDictionary<string, string> Parameters { get; set; }
        public string ClassSelect { get; set; }
        public string OnChange { get; set; }
        public string PageAction { get; set; }
        public string ValuesLink { get; set; }
        public string Parameter { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            output.Attributes.Add("class", ClassSelect);
            output.Attributes.Add("onchange", OnChange);

            foreach (KeyValuePair<string, string> parameter in Parameters)
            {
                TagBuilder option = new TagBuilder("option");
                option.Attributes["value"] = PageAction + GetValuesLink(parameter.Key, Parameter);
                if (parameter.Key == GetValueParameterLink(ValuesLink, Parameter))
                {
                    option.Attributes["selected"] = "selected";
                }
                option.InnerHtml.Append(parameter.Value);
                output.Content.AppendHtml(option);
            }
        }
 
        // Return correct parameter GET-request`s to select count products.
        private string GetValuesLink(string value, string parameter)
        {
            if (ValuesLink == "")
            {
                return "?" + parameter + "=" + value;
            }
            
            Regex reg = new Regex(parameter + @"=\w+");
            
            if (ValuesLink != "" && reg.Matches(ValuesLink).Count == 0)
            {
                return ValuesLink + "&" + parameter + "=" + value;
            }

            return reg.Replace(ValuesLink,  parameter + "=" + value);
        }

        private string GetValueParameterLink(string link, string value)
        {
            if (link == "")
            {
                return Parameters.First().Key;
            }
            
            Regex reg = new Regex(value + @"=\w+");
            
            return reg.Match(ValuesLink).Value.Split("=").Last();
        }
    }
}