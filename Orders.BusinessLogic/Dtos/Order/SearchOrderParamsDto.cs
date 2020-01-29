namespace Orders.BusinessLogic.Dtos.Order
{
    public class SearchOrderParamsDto
    {
        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public double MinimumPrice { get; set; }

        public double? MaximumPrice { get; set; }

        public bool IsPriceSortingAscending { get; set; }

        public int? PageNumber { get; set; } = Constants.Constants.INITIAL_PAGE_NUMBER;

        public int? NumberOfElementsOnPage { get; set; } = Constants.Constants.DEFAULT_NUMBER_OF_ELEMENTS_ON_PAGE;
    }
}
