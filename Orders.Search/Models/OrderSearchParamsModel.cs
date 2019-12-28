namespace Orders.Search.Models
{
    public class OrderSearchParamsModel
    {
        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public double MinimumPrice { get; set; }

        public double? MaximumPrice { get; set; }

        public bool IsPriceSortingAscending { get; set; }

        public int? PageNumber { get; set; }

        public int? NumberOfElementsOnPage { get; set; }
    }
}
