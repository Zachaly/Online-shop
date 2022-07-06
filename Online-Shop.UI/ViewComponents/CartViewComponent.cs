﻿using Microsoft.AspNetCore.Mvc;
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
        private AppDbContext _dbContext;

        public CartViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view, new GetCart(HttpContext.Session, _dbContext).Execute());
        }
    }
}
