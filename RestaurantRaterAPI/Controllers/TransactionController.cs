using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    [RoutePrefix("api/transaction")]
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();
        // POST api/transaction
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Transaction transactionToCreate)
        {
            Transaction createdTransaction = _ctx.Transactions.Add(transactionToCreate);
            try
            {
                await _ctx.SaveChangesAsync();
                return Ok(createdTransaction);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET api/transaction
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _ctx.Transactions.ToListAsync();

            return Ok(transactions);
        }
        // GET api/transaction/:id
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetSingle(int id)
        {
            //Transaction Transaction = _ctx.Transactions.Where(prod => prod.Name == name).FirstOrDefault();

            Transaction requestedTransaction = await _ctx.Transactions.FindAsync(id);

            if (requestedTransaction == null)
            {
                return NotFound();
            }
            return Ok(requestedTransaction);
        }
        // PUT api/transaction/:id
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] Transaction updatedTransaction)
        {
            Transaction requestedTransaction = await _ctx.Transactions.FindAsync(id);

            if (requestedTransaction == null)
            {
                return NotFound();
            }
            if (updatedTransaction.TransactionID != 0)
            {
                requestedTransaction.TransactionID = updatedTransaction.TransactionID;
            }
            if (updatedTransaction.CustomerID != 0)
            {
                requestedTransaction.CustomerID = updatedTransaction.CustomerID;
            }
            
            try
            {
                await _ctx.SaveChangesAsync();
                return Ok(requestedTransaction);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // DELETE api/Transaction/:id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            Transaction requestedTransaction = await _ctx.Transactions.FindAsync(id);

            if (requestedTransaction == null)
            {
                return NotFound();
            }
            try
            {
                _ctx.Transactions.Remove(requestedTransaction);
                await _ctx.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
