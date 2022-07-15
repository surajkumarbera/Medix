﻿using MediX.Context;
using MediX.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace MediX.Controllers
{
    public class OrderDetailsController : ApiController
    {
        private MediX_DbContext db = new MediX_DbContext();

        // GET: api/OrderDetails
        public IQueryable<OrderDetails> GetOrderDetails()
        {
            return db.OrderDetails;
        }

        // GET: api/OrderDetails/5
        [ResponseType(typeof(OrderDetails))]
        public IHttpActionResult GetOrderDetails(int id)
        {
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return Ok(orderDetails);
        }
        /*
        // PUT: api/OrderDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderDetails(int id, OrderDetails orderDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderDetails.Id)
            {
                return BadRequest();
            }

            db.Entry(orderDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        */



        // POST: api/OrderDetails
        [ResponseType(typeof(OrderDetails))]
        public IHttpActionResult PostOrderDetails(OrderDetails orderDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails.Add(orderDetails);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderDetails.Id }, orderDetails);
        }

        // DELETE: api/OrderDetails/5
        [ResponseType(typeof(OrderDetails))]
        public IHttpActionResult DeleteOrderDetails(int id)
        {
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(orderDetails);
            db.SaveChanges();

            return Ok(orderDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailsExists(int id)
        {
            return db.OrderDetails.Count(e => e.Id == id) > 0;
        }
    }
}