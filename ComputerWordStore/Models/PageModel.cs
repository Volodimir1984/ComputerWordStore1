using System;

namespace ComputerWordStore.Models
{
    public class PageModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        
        public PageModel(int pageNumber, int totalPages)
        {
            PageNumber = pageNumber;
            TotalPages = totalPages;
        }

        public bool HasPreviousPage
        {
            get
            {
                return PageNumber > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}