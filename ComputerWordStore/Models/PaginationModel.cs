using System.Collections.Generic;
using System.Collections.Immutable;
using ComputerWordStore.Models.Products;

namespace ComputerWordStore.Models
{
    public class PaginationModel
    {
        public PaginationModel()
        {
            CountProductOnPage = new Dictionary<string, string>
            {
                {"12", "12"},
                {"16", "16"},
                {"20", "20"}
            };
            
            SortingParameters = new Dictionary<string, string>
            {
                {"cheap", "Від дешевших до дорогих"},
                {"expensive", "Від дорогих до дешевих"}
            };
        }
        
        public IDictionary<string, string> CountProductOnPage { get; }
        public IDictionary<string, string> SortingParameters { get; }
        public IEnumerable<Product> Products { get; set; }
        public PageModel PageModel { get; set; }
        
    }
}