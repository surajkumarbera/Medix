using MediX.Context;
using MediX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MediX.Controllers
{
    public class CartsController : ApiController
    {
        private MediX_DbContext db = new MediX_DbContext();

        [HttpGet]
        [Route("api/Carts")]
        public IHttpActionResult GetCarts()
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            List<Object> cartList = new List<Object>();

            foreach (var cart in db.Carts)
            {
                cart.Products.ImageUrl = ImageHandler.ImageCdnUrl + "/" + cart.Products.ImageUrl;
                cartList.Add(cart);
            }

            return Ok(cartList);
        }

        [HttpGet]
        [Route("api/Carts/{id}")]
        public IHttpActionResult GetCarts(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            Carts carts = db.Carts.Find(id);
            if (carts == null)
            {
                return NotFound();
            }

            return Ok(carts);
        }


        [HttpGet]
        [Route("api/Carts/User/{id}")]
        public IHttpActionResult GetCartsbyUSer(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            var carts = db.Carts.Where(x => x.UserId == id);
            if (carts == null)
            {
                return NotFound();
            }


            List<Object> cartList = new List<Object>();

            foreach (var cart in carts)
            {
                cart.Products.ImageUrl = ImageHandler.ImageCdnUrl + "/" + cart.Products.ImageUrl;
                cartList.Add(cart);
            }

            return Ok(cartList);
        }
        /*
        [HttpPut]
        [Route("api/Carts/{id}")]
        public IHttpActionResult PutCarts(int id, Carts carts)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carts.Id)
            {
                return BadRequest();
            }

            try
            {
                db.Entry(carts).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(carts);
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!CartsExists(id))
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
        [Route("api/Carts")]
        public IHttpActionResult PostCarts(Carts carts)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Carts.Add(carts);
                db.SaveChanges();

                return Ok(carts);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete]
        [Route("api/Carts/{id}")]
        public IHttpActionResult DeleteCarts(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            Carts carts = db.Carts.Find(id);
            if (carts == null)
            {
                return NotFound();
            }

            db.Carts.Remove(carts);
            db.SaveChanges();

            return Ok(carts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartsExists(int id)
        {
            return db.Carts.Count(e => e.Id == id) > 0;
        }
    }
}