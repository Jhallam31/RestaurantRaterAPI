using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //Read All
        [HttpGet]
        public IHttpActionResult Index()
        {
            List<Customer> customerInDb = _context.Customers.ToList();
            return Ok(customerInDb);
        }

        //Read Single
        [HttpGet]
        public IHttpActionResult GetByID(int id)
        {
            Customer requestedCustomer = _context.Customers.Find(id);
            if(requestedCustomer == null)
            {
                return NotFound();
            }

            return Ok(requestedCustomer);
        }

        //Create
        [HttpPost]
        public IHttpActionResult Create(Customer customerToAdd)
        {
            _context.Customers.Add(customerToAdd);
            if(_context.SaveChanges()==1)
            {
                return Ok(customerToAdd);
            }
            return BadRequest();
        }

        //Update
        [HttpPut]
        public IHttpActionResult Update(Customer updatedCustomer)
        {
            Customer requestedCustomer = _context.Customers.Find(updatedCustomer.CustomerID);
            if(requestedCustomer == null)
            {
                return BadRequest("Invalid ID");
            }

            requestedCustomer.FirstName = updatedCustomer.FirstName;
            requestedCustomer.LastName = updatedCustomer.LastName;
            requestedCustomer.Age = updatedCustomer.Age;

            if(_context.SaveChanges()==1)
            {
                return Ok(updatedCustomer);
            }

            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            Customer customer = _context.Customers.Find(id);
            if(customer == null)
            {
                return BadRequest("Invalid ID");
            }
            _context.Customers.Remove(customer);
            if(_context.SaveChanges() == 1)
            {
                return Ok("Successfully removed customer from database");
            }

            return BadRequest();
        }
    }
}
