using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        // ----------------- Get Requests ------------------
        [HttpGet("")]
        public IActionResult Index(DashboardWrapper newView)
        {
            if (newView.Search == null)
            {
                newView.RecentProducts = dbContext.Products
                .OrderByDescending(p => p.CreatedAt).Take(5)
                .ToList();
            }
            else
            {
                newView.RecentProducts = dbContext.Products
                .Where(p => p.ProductName.Contains(newView.Search) || p.ProductName.Contains(newView.Search))
                .OrderByDescending(p => p.CreatedAt).Take(5)
                .ToList();
            }
            newView.RecentOrders = dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .OrderByDescending(o => o.CreatedAt).Take(3)
                .ToList();

            newView.RecentCustomers = dbContext.Customers
                .OrderByDescending(c => c.CreatedAt).Take(3)
                .ToList();
            return View(newView);
        }

        [HttpGet("products")]
        public IActionResult Products(ProdWrapper newView)
        {
            if (newView.Search == null)
            {
                newView.AllProducts = dbContext.Products.ToList();
            }
            else
            {
                newView.AllProducts = dbContext.Products
                .Where(p => p.ProductName.Contains(newView.Search) || p.ProductName.Contains(newView.Search))
                .ToList();
            }            
            return View(newView);
        }

        [HttpGet("customers")]
        public IActionResult Customers()
        {
            CustWrapper newView = new CustWrapper();
            newView.AllCustomers = dbContext.Customers.ToList();
            return View(newView);
        }

        [HttpGet("orders")]
        public IActionResult Orders()
        {
            OrdersWrapper newView = new OrdersWrapper();
            newView.AllCustomers = dbContext.Customers.ToList();
            newView.AllProducts = dbContext.Products.ToList();
            newView.AllOrders = dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .ToList();
            return View(newView);
        }
        
        // ---------------- Post Requests ------------------------------
        // ---------------- Add Models ---------------------------------
        [HttpPost("addCustomer")]
        public IActionResult AddCustomer(CustWrapper fromForm)
        {
            CustWrapper newView = new CustWrapper();
            if(ModelState.IsValid)
            {
                if(dbContext.Customers.Any(c => c.CustomerName == fromForm.Customer.CustomerName))
                {
                    ModelState.AddModelError("Customer.CustomerName","Customer already exists");
                    newView.AllCustomers = dbContext.Customers.ToList();
                    return View("Customers",newView);
                }
                dbContext.Add(fromForm.Customer);
                dbContext.SaveChanges();
                return RedirectToAction("Customers");
            }
            newView.AllCustomers = dbContext.Customers.ToList();
            return View("Customers",newView);
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct(ProdWrapper fromForm)
        {
            ProdWrapper newView = new ProdWrapper();
            if(ModelState.IsValid)
            {
                if(dbContext.Products.Any(p => p.ProductName == fromForm.Product.ProductName))
                {
                    ModelState.AddModelError("Product.ProductName","Please use different product name.");
                    newView.AllProducts = dbContext.Products.ToList();
                    return View(newView);
                }
                dbContext.Add(fromForm.Product);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            return RedirectToAction("Products");
        }

        [HttpPost("addOrder")]
        public IActionResult AddOrder(OrdersWrapper fromForm)
        {
            Product thisProduct = dbContext.Products
                .FirstOrDefault(p => p.ProductId == fromForm.Order.ProductId);
            if(ModelState.IsValid)
            {
                if(fromForm.Order.OrderQty > thisProduct.ProductQty)
                {
                    ModelState.AddModelError("Order.OrderQty",$"Only {thisProduct.ProductQty} items in stock.");
                    return View("Orders");
                }
                thisProduct.ProductQty -= fromForm.Order.OrderQty;
                dbContext.Add(fromForm.Order);
                dbContext.SaveChanges();
                return RedirectToAction("Orders");
            }    
            return RedirectToAction("Orders");
        }


        // ---------------- Privacy and Error View Model ----------------
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
