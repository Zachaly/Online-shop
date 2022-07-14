
namespace Online_Shop.Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string GetPriceString(this decimal value) => $"{value.ToString("N2")}$";
    }
}
