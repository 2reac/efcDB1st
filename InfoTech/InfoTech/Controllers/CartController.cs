using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoTech.Models;
using InfoTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoTech.Controllers
{
    public class CartController : Controller
    {

        private readonly ITContext _context;

        public CartController(ITContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        public async Task<IActionResult> MyCart()
        {
            var customer = _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).First();
            var ordering = _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefault();

            List<CartProductViewModel> myProducts = new List<CartProductViewModel>();
            if (ordering != null)
            {
                var products = _context.OrderProduct.Where(o => o.OrderId.Equals(ordering.OrderId)).ToList();

                foreach (OrderProduct product in products)
                {
                    var prod = _context.Product.Where(p => p.ProductId.Equals(product.ProductId)).First();
                    CartProductViewModel myProduct = new CartProductViewModel();
                    myProduct.Id = prod.ProductId;
                    myProduct.Name = prod.ProductName;
                    myProduct.Price = (decimal)prod.ProductPrice;
                    myProduct.Quantity = product.Quantity;
                    myProduct.Image = prod.ProductImage;
                    myProducts.Add(myProduct);
                }
            }
            return View(myProducts);
        }
        public async Task<IActionResult> AddProduct(decimal Price, int Id)
        {
            var customer = _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).First();
            var ordering = _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefault();

            if (ordering != null)
            {
                var product = _context.OrderProduct.Where(i => i.ProductId.Equals(Id) && i.OrderId.Equals(ordering.OrderId)).FirstOrDefault();

                if (product != null)
                {
                    product.Quantity++;
                }
                else
                {
                    OrderProduct order_product = new OrderProduct();
                    order_product.OrderId = ordering.OrderId;
                    order_product.ProductId = Id;
                    order_product.Quantity = 1;
                    _context.OrderProduct.Add(order_product);
                }
                _context.SaveChanges();
            }
            else
            {
                Order order = new Order();
                order.OrderStatus = "Cart";
                order.CustomerId = customer;
                _context.Order.Add(order);
                _context.SaveChanges();

                var order_id = _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).Select(e => e.OrderId).First();

                OrderProduct order_product = new OrderProduct();
                order_product.OrderId = order_id;
                order_product.ProductId = Id;
                order_product.Quantity = 1;
                _context.OrderProduct.Add(order_product);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
