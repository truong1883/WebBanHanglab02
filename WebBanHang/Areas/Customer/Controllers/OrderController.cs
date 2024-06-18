using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Helper;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        { 
            _db = db;
        }
        public IActionResult Index()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (cart == null)
            {
                cart = new Cart();
            }
            ViewBag.Cart = cart;
            return View(cart);
        }
        public IActionResult ProcessOrder(Order order)
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                order.Total = cart.Total;
                order.State  ="Pending";

                // them order vao csdl
                _db.Orders.Add(order);
                _db.SaveChanges();

                //them orderDtail vao csdl
                foreach (var item in cart.Items)
                {
                    var orderDtail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductID = item.Product.Id,
                        Quantity = item.Quantity
                    };
                    _db.orderDetails.Add(orderDtail);
                    _db.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                return View("Result");
            }
            ViewBag.CART = cart;
            return View("Index",order);
        }
    }
}
