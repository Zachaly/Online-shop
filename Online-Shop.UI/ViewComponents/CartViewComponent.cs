using Microsoft.AspNetCore.Mvc;
using Online_shop.DataBase;
using Online_Shop.Application.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke([FromServices] GetCart getCart, string view = "Default")
        {
            if(view == "Small")
            {
                var value = getCart.Execute().
                    Sum(product => product.RealValue * product.Quantity);
                return View(view, $"{value}$");
            }

            return View(view, getCart.Execute());
        }
    }
}
