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

        public IActionResult MyCart()
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

        public IActionResult AddProduct(int Id)
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
                    //object initialization
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
                //object initialization simplified
                Order order = new Order
                {
                    OrderStatus = "Cart",
                    CustomerId = customer
                };
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

            return RedirectToAction("Index", "Products");
        }

        public IActionResult RemoveProduct(int Id)
        {
            var customer = _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).First();
            var ordering = _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefault();

            if (ordering != null)
            {
                var product = _context.OrderProduct.Where(i => i.ProductId.Equals(Id) && i.OrderId.Equals(ordering.OrderId)).FirstOrDefault();
                _context.OrderProduct.Remove(product);
                _context.SaveChangesAsync();
            }

            return RedirectToAction("MyCart", "Cart");
        }

        public IActionResult AddToCart(int Id)
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
                    //think this is not needed
                    OrderProduct order_product = new OrderProduct();
                    order_product.OrderId = ordering.OrderId;
                    order_product.ProductId = Id;
                    order_product.Quantity = 1;
                    _context.OrderProduct.Add(order_product);
                }
                _context.SaveChangesAsync();
            }

            return RedirectToAction("MyCart", "Cart");
        }

        public IActionResult RemoveFromCart(int Id)
        {
            var customer = _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).First();
            var ordering = _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefault();

            if (ordering != null)
            {
                var product = _context.OrderProduct.Where(i => i.ProductId.Equals(Id) && i.OrderId.Equals(ordering.OrderId)).FirstOrDefault();

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
                _context.SaveChangesAsync();
            }

            return RedirectToAction("MyCart", "Cart");
        }

        public IActionResult FinishOrder()
        {
            var customer = _context.Customer.Where(e => e.Email.Equals(User.Identity.Name)).Select(e => e.CustomerId).First();
            var ordering = _context.Order.Where(i => i.CustomerId.Equals(customer) && i.OrderStatus.Equals("Cart")).FirstOrDefault();

            if (ordering != null)
            {
                ordering.OrderStatus = "Placed";
                _context.Update(ordering);
                _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Products");
        }
    }
}
