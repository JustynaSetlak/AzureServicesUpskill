using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Dtos.Order
{
    public class SearchOrderParamsDto
    {
        public string ProductName { get; set; }

        public int CategoryName { get; set; }

        public double MinimumPrice { get; set; }

        public double MaximumPrice { get; set; }

        public bool IsPriceSortingAscending { get; set; }

        public int PageNumber { get; set; }

        public int NumberOfElementsOnPage { get; set; }
    }
}
