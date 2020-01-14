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
            var customer = await _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).FirstAsync();
            var ordering = await _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefaultAsync();

            List<CartProductViewModel> myProducts = new List<CartProductViewModel>();
            if (ordering != null)
            {
                var products = _context.OrderProduct.Where(o => o.OrderId.Equals(ordering.OrderId)).ToList();

                foreach (OrderProduct product in products)
                {
                    var prod = await _context.Product.Where(p => p.ProductId.Equals(product.ProductId)).FirstAsync();
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

        public async Task<IActionResult> AddProduct(int id)
        {
            var customer = await _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).FirstAsync();
            var ordering = await _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefaultAsync();

            if (ordering != null)
            {
                var product = await _context.OrderProduct.Where(i => i.ProductId.Equals(id) && i.OrderId.Equals(ordering.OrderId)).FirstOrDefaultAsync();

                if (product != null)
                {
                    product.Quantity++;
                }
                else
                {
                    //object initialization
                    OrderProduct order_product = new OrderProduct();
                    order_product.OrderId = ordering.OrderId;
                    order_product.ProductId = id;
                    order_product.Quantity = 1;
                    _context.OrderProduct.Add(order_product);
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                //object initialization simplified
                Order order = new Order
                {
                    OrderStatus = "Cart",
                    CustomerId = customer
                };
                _context.Order.Add(order);
                _context.SaveChanges();

                var order_id = await _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).Select(e => e.OrderId).FirstAsync();

                OrderProduct order_product = new OrderProduct();
                order_product.OrderId = order_id;
                order_product.ProductId = id;
                order_product.Quantity = 1;
                _context.OrderProduct.Add(order_product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Products");
        }

        public async Task<IActionResult> RemoveProduct(int id)
        {
            var customer = await _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).FirstAsync();
            var ordering = await _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefaultAsync();

            if (ordering != null)
            {
                var product = await _context.OrderProduct.Where(i => i.ProductId.Equals(id) && i.OrderId.Equals(ordering.OrderId)).FirstOrDefaultAsync();
                _context.OrderProduct.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MyCart", "Cart");
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var customer = await _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).FirstAsync();
            var ordering = await _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefaultAsync();

            if (ordering != null)
            {
                var product = await _context.OrderProduct.Where(i => i.ProductId.Equals(id) && i.OrderId.Equals(ordering.OrderId)).FirstOrDefaultAsync();

                if (product != null)
                {
                    product.Quantity++;
                }
                else
                {
                    //think this is not needed
                    OrderProduct order_product = new OrderProduct();
                    order_product.OrderId = ordering.OrderId;
                    order_product.ProductId = id;
                    order_product.Quantity = 1;
                    _context.OrderProduct.Add(order_product);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MyCart", "Cart");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var customer = await _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).FirstAsync();
            var ordering = await _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefaultAsync();

            if (ordering != null)
            {
                var product = await _context.OrderProduct.Where(i => i.ProductId.Equals(id) && i.OrderId.Equals(ordering.OrderId)).FirstOrDefaultAsync();

                if (product != null)
                {
                    if (product.Quantity == 1)
                    {
                        _context.OrderProduct.Remove(product);
                    }
                    else
                    {
                        product.Quantity--;
                    }
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MyCart", "Cart");
        }

        public async Task<IActionResult> FinishOrder()
        {
            var customer = await _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).FirstAsync();
            var ordering = await _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefaultAsync();

            if (ordering != null)
            {
                ordering.OrderStatus = "Placed";
                _context.Update(ordering);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Products");
        }
    }
}
