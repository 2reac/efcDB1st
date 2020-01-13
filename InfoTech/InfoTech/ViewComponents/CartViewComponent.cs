using InfoTech.Models;
using InfoTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTech.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public readonly ITContext _context;
        public CartViewModel model = new CartViewModel();

        public CartViewComponent(ITContext context)
        {
            _context = context;
        }

        public IViewComponentResult InvokeAsync()
        {
            var customer = _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).First();
            var ordering = _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefault();

            model.ItemsNo = 0;
            model.TotalPrice = 0;
            if(ordering != null) {

                var products = _context.OrderProduct.Where(o => o.OrderId.Equals(ordering.OrderId)).ToList();

                foreach (OrderProduct product in products)
                {
                    model.ItemsNo += product.Quantity;
                    var price = _context.Product.Where(p => p.ProductId.Equals(product.ProductId)).Select(p => p.ProductPrice).First();
                    model.TotalPrice += product.Quantity * (decimal)price;
                }
            }

            return View(model);
        }
    }
}
