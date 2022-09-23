using System;

namespace cars.ViewModels
{
    public class FilterCarViewModel
    {
        public string brand { get; set; }
        public string model { get; set; }

        public uint? minPrice { get; set; }
        public uint? maxPrice { get; set; }

        public string categoryName { get; set; }
    }
}
