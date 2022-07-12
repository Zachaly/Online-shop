using Microsoft.AspNetCore.Mvc;
using Online_Shop.Application.Cart;
using System.Linq;

namespace Online_Shop.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private GetCart _getCart;

        public CartViewComponent([FromServices] GetCart getCart)
        {
            _getCart = getCart;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            if(view == "Small")
            {
                var value = _getCart.Execute().
                    Sum(product => product.RealValue * product.Quantity);
                return View(view, $"{value}$");
            }

            return View(view, _getCart.Execute());
        }
    }
}
