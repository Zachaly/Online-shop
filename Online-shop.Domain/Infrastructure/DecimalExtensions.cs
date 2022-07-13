using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_shop.Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string GetPriceString(this decimal value) => $"{value.ToString("N2")}$";
    }
}
