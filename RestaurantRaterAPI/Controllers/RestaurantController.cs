using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    [RoutePrefix("api/restaurant")]
    public class RestaurantController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        
        //Read All
        [HttpGet]
        public IHttpActionResult Index()
        {
            List<Restaurant> restaurantsInDB = _context.Restaurants.ToList();
            return Ok(restaurantsInDB);
        }

        //Read Single
        [HttpGet]
        public IHttpActionResult GetByID(int id)
        {
            Restaurant requestedRestaurant = _context.Restaurants.Find(id);
            if(requestedRestaurant == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(requestedRestaurant);
            }
        }

        //Create
        [HttpPost]
        public IHttpActionResult Create(Restaurant restaurantToAdd)
        {
            _context.Restaurants.Add(restaurantToAdd);
            if (_context.SaveChanges()==1)
            {
                return Ok(restaurantToAdd);
            }
            else
            {
                return BadRequest();
            }
        }

        //Update
        [HttpPut]
        public IHttpActionResult Update(Restaurant updatedRestaurant)
        {
            Restaurant restaurant = _context.Restaurants.Find(updatedRestaurant.RestaurantId);
            if(restaurant == null)
            {
                return BadRequest("Invalid ID");
            }
            restaurant.Name = updatedRestaurant.Name;
            restaurant.Rating = updatedRestaurant.Rating;

            if(_context.SaveChanges()==1)
            {
                return Ok(updatedRestaurant);
            }

            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            Restaurant restaurant = _context.Restaurants.Find(id);
            if(restaurant == null)
            {
                return BadRequest("Invalid ID");
            }

            _context.Restaurants.Remove(restaurant);
            if(_context.SaveChanges()== 1)
            {
                return Ok("Successfully removed restaurant from database.");
            }

            return BadRequest();
        }
    }
}
