using System.Threading.Tasks;
using ComputerWordStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ComputerWordStore.TagHelpers
{
    // Tag helper for pagination views.
    public class PaginationTagHelper : TagHelper
    {
        private const int CountTag = 9;
        private const int LimitLinkPage = 5;

        public PaginationTagHelper(IUrlHelperFactory helperFactory) {}
        
        public PageModel PageModel { get; set; }
        public string PageLink { get; set; }
        public string ListClass { get; set; }
        public string DivNextPageClass { get; set; }
        public string PreviousPageClass { get; set; }
        public string NextPageClass { get; set; }
        public string ActivePageClass { get; set; }
        public string ValuesLink { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            output.TagName = "nav";

            TagBuilder tag = new TagBuilder("ul");
            tag.Attributes.Add("class", ListClass);

            if (PageModel.HasPreviousPage)
            {
                tag.InnerHtml
                    .AppendHtml(CreateNextPageTag(PreviousPageClass, 
                        PageLink + (PageModel.PageNumber - 1) + ValuesLink));
            }
            
            output.Content.AppendHtml(CreateListPagination(tag));

            if (PageModel.HasNextPage)
            {
                tag.InnerHtml
                    .AppendHtml(CreateNextPageTag(NextPageClass, 
                        PageLink + (PageModel.PageNumber + 1) + ValuesLink));
            }
        }

        // Return tags li and a for pagination list.
        private  TagBuilder GetLiAndAnchorTag(int pageNumber, string pageLink)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");

            if (PageModel.PageNumber == pageNumber)
            {
                item.Attributes.Add("class", ActivePageClass);
            }

            link.InnerHtml.Append(pageNumber.ToString());
            link.Attributes["href"] = pageLink;
            item.InnerHtml.AppendHtml(link);

            return item;
        }
        
        private  TagBuilder GetLiAndAnchorTag(string pageNumber, string pageLink) 
        {
            TagBuilder item = new TagBuilder("li");
            item.Attributes["href"] = pageLink;
                    
            item.InnerHtml.Append(pageNumber);
        
            return item;
        }

        // Return tags for widgets next page and previous page.
        private TagBuilder CreateNextPageTag(string classContent, string pageLink)
        {
            TagBuilder page = new TagBuilder("li");
            page.Attributes.Add("class", DivNextPageClass);
            
            TagBuilder link = new TagBuilder("a");
            link.Attributes["href"] = pageLink;
                
            TagBuilder iPage = new TagBuilder("i");
            iPage.Attributes.Add("class", classContent);
            link.InnerHtml.AppendHtml(iPage);

            page.InnerHtml.AppendHtml(link);

            return page;
        }

        // Return tag with pagination list.
        private TagBuilder CreateListPagination(TagBuilder tag)
        {
            for (int i = 0; i < PageModel.TotalPages; i++)
            {
                if (PageModel.TotalPages > CountTag)
                {
                    if (PageModel.PageNumber <= LimitLinkPage)
                    {
                        if (i == PageModel.TotalPages - 2)
                        {
                            tag.InnerHtml.AppendHtml(GetLiAndAnchorTag("...", "#"));
                            continue;
                        }

                        if (i >= CountTag - 2 && i <= PageModel.TotalPages - 3)
                        {
                            continue;
                        }
                    }
                    else if (PageModel.PageNumber > LimitLinkPage && PageModel.PageNumber <= PageModel.TotalPages - 5)
                    {
                        if (i == 1 || i == PageModel.TotalPages - 2)
                        {
                            tag.InnerHtml.AppendHtml(GetLiAndAnchorTag("...", "#"));
                            continue;
                        }

                        if ((i > PageModel.PageNumber + 1 || i < PageModel.PageNumber - 3)
                            && i <= PageModel.TotalPages - 3 && i != 0)
                        {
                            continue;
                        }
                    }
                    else if (PageModel.PageNumber >= PageModel.TotalPages - CountTag + 2)
                    {
                        if (i == 1)
                        {
                            tag.InnerHtml.AppendHtml(GetLiAndAnchorTag("...", "#"));
                            continue;
                        }
                        
                        if (i <= PageModel.TotalPages - CountTag + 1 && i != 0)
                        {
                            continue;
                        }
                    }
                }
                tag.InnerHtml.AppendHtml(GetLiAndAnchorTag(i + 1, PageLink + (i + 1) + ValuesLink));
            }

            return tag;
        }

    }
}