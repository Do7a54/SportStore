using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;
using static System.Net.WebRequestMethods;
using System.Diagnostics;

using System.Reflection.Metadata;

using System;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }
        public ViewResult Checkout() => View(new Order());   // Modified Page 235
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
            else
            {
                return View();
            }             // Page 235 to -- to modify the constructor so that it receives the services it requires to   process an order and add an action method that will handle the HTTP form POST request when the user clicks the Complete Order button.
        }
    }
}