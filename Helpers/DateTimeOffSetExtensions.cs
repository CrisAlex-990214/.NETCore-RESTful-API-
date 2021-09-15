using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Helpers
{
    public static class DateTimeOffSetExtensions
    {

        public static string GetPurchasedDate(this DateTimeOffset dateTimeOffset)
        {        
            return $"{dateTimeOffset.Day}-{dateTimeOffset.Month}-{dateTimeOffset.Year}";
        }

    }
}
