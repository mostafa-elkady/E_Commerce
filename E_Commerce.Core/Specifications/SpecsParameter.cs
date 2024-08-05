using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Commerce.Core.Specifications
{
    public class SpecsParameter
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        // Search
        public string? Search { get => _Search; set => _Search = value?.Trim().ToLower(); }
        private string? _Search;
        //Pagination
        private const int MAXPAGESIZE = 10;

        private int _pageSize = 10; // Default page size is 10

        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value;
        }



        //Sorting
        [EnumDataType(typeof(SortOptions))]
        public SortOptions SortBy { get; set; }
    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortOptions
    {

        NameAscending,
        NameDescending,
        PriceAscending,
        PriceDescending
    }
}
