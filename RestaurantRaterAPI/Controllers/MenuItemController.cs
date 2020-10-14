using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    [RoutePrefix("api/MenuItem")]
    public class MenuItemController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //Read All
        [HttpGet]
        public IHttpActionResult Index()
        {
            List<MenuItem> menuItemsInDB = _context.MenuItems.ToList();
            return Ok(menuItemsInDB);
        }

        //Read Single
        [HttpGet]
        public IHttpActionResult GetByID(int id)
        {
            MenuItem requestedMenuItem = _context.MenuItems.Find(id);
            if(requestedMenuItem == null)
            {
                return NotFound();
            }

            return Ok(requestedMenuItem);
        }

        //Create
        [HttpPost]
        public IHttpActionResult Create(MenuItem itemToAdd)
        {
            _context.MenuItems.Add(itemToAdd);
            if(_context.SaveChanges()==1)
            {
                return Ok(itemToAdd);
            }
            return BadRequest();
        }

        //Update
        [HttpPut]
        public IHttpActionResult Update(MenuItem updatedItem)
        {
            MenuItem requestedMenuItem = _context.MenuItems.Find(updatedItem.MenuItemID);
            if(requestedMenuItem == null)
            {
                return BadRequest("Invalid ID");
            }
            requestedMenuItem.ItemName = updatedItem.ItemName;
            requestedMenuItem.Description = updatedItem.Description;
            requestedMenuItem.Price = updatedItem.Price;

            if(_context.SaveChanges()==1)
            {
                return Ok(updatedItem);
            }

            return BadRequest();

        }

        //Delete
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            MenuItem menuItem = _context.MenuItems.Find(id);
            if(menuItem == null)
            {
                return BadRequest("Invalid ID");
            }
            _context.MenuItems.Remove(menuItem);
            if(_context.SaveChanges()==1)
            {
                return Ok("Successfully removed menu item from database");
            }

            return BadRequest();
        }

    }
}
