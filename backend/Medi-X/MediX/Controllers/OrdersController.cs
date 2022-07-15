using MediX.Context;
using MediX.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MediX.Controllers
{
    public class OrdersController : ApiController
    {
        private MediX_DbContext db = new MediX_DbContext();

        [HttpGet]
        [Route("api/Orders")]
        public IHttpActionResult GetOrders()
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            return Ok(db.Orders);
        }

        [HttpGet]
        [Route("api/Orders/{id}")]
        public IHttpActionResult GetOrders(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        /*
        [HttpPut]
        [Route("api/Orders/{id}")]
        public IHttpActionResult PutOrders(int id, Orders orders)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orders.Id)
            {
                return BadRequest();
            }

            try
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(orders);
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!OrdersExists(id))
                {
                    return NotFound();
                }
                else
                {

                    return InternalServerError(e);
                }
            }

        }
        */

        [HttpPost]
        [Route("api/Orders")]
        public IHttpActionResult PostOrders()
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            try
            {

                int UserId = int.Parse(HttpContext.Current.Request.Form["userId"]);

                string imageUrl = ImageHandler.PostToCdn(Request);

                Prescriptions prescriptions = new Prescriptions
                {
                    UserId = UserId,
                    ImageUrl = imageUrl,
                    Verified = false
                };
                db.Prescriptions.Add(prescriptions);
                db.SaveChanges();

                Orders orders = new Orders()
                {
                    UserId = UserId,
                    PrescriptionsId = prescriptions.Id,
                    DateTime = DateTime.Now,
                };
                db.Orders.Add(orders);
                db.SaveChanges();

                var carts = db.Carts.Where(x => x.UserId == UserId);
                foreach (var cart in carts)
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        OrderId = orders.Id,
                        ProductsId = cart.ProductsId,
                        Quantity = cart.Quantity
                    };
                    db.Carts.Remove(cart);
                    db.OrderDetails.Add(orderDetails);
                }
                db.SaveChanges();

                return Ok(carts);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete]
        [Route("api/Orders/{id}")]
        public IHttpActionResult DeleteOrders(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }

            db.Orders.Remove(orders);
            db.SaveChanges();

            return Ok(orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdersExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}