using System.Collections.Generic;
using System.Linq;

namespace CardLearnApp.Data.DataSeach
{
    public static class SearchElement
    {
        public static List<string> ByName(List<string> reference, string value) 
            => (from element in reference where element.ToLower().Contains(value.ToLower()) select element).ToList();
    }
}