using InfoTech.Models;
using InfoTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customer = await _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).FirstAsync();
            var ordering = await _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefaultAsync();

            model.ItemsNo = 0;
            model.TotalPrice = 0;
            if(ordering != null) {

                var products = await _context.OrderProduct.Where(o => o.OrderId.Equals(ordering.OrderId)).ToListAsync();

                foreach (OrderProduct product in products)
                {
                    model.ItemsNo += product.Quantity;
                    var price = await _context.Product.Where(p => p.ProductId.Equals(product.ProductId)).Select(p => p.ProductPrice).FirstAsync();
                    model.TotalPrice += product.Quantity * (decimal)price;
                }
            }

            return View(model);
        }
    }
}
