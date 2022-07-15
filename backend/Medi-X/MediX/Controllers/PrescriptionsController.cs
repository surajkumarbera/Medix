using MediX.Context;
using MediX.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MediX.Controllers
{
    public class PrescriptionsController : ApiController
    {
        private MediX_DbContext db = new MediX_DbContext();

        [HttpGet]
        [Route("api/Prescriptions")]
        public IHttpActionResult GetPrescriptions()
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            return Ok(db.Prescriptions);
        }

        [HttpGet]
        [Route("api/Prescriptions/{id}")]
        public IHttpActionResult GetPrescriptions(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            Prescriptions prescriptions = db.Prescriptions.Find(id);
            if (prescriptions == null)
            {
                return NotFound();
            }

            return Ok(prescriptions);
        }
        /*
        [HttpPut]
        [Route("api/Prescriptions/{id}")]
        public IHttpActionResult PutPrescriptions(int id, Prescriptions prescriptions)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescriptions.Id)
            {
                return BadRequest();
            }

            try
            {
                db.Entry(prescriptions).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(prescriptions);
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!PrescriptionsExists(id))
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
        // POST: api/Prescriptions
        [HttpPost]
        [Route("api/Prescriptions")]
        public IHttpActionResult PostPrescriptions()
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            try
            {
                Prescriptions prescriptions1 = new Prescriptions();
                prescriptions1.UserId = int.Parse(HttpContext.Current.Request.Form["userId"]);
                prescriptions1.Verified = false;
                prescriptions1.ImageUrl = ImageHandler.PostToCdn(Request);

                db.Prescriptions.Add(prescriptions1);
                db.SaveChanges();

                return Ok(prescriptions1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete]
        [Route("api/Prescriptions/{id}")]
        public IHttpActionResult DeletePrescriptions(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized) return BadRequest("Unauthorized");

            Prescriptions prescriptions = db.Prescriptions.Find(id);
            if (prescriptions == null)
            {
                return NotFound();
            }

            db.Prescriptions.Remove(prescriptions);
            db.SaveChanges();

            return Ok(prescriptions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrescriptionsExists(int id)
        {
            return db.Prescriptions.Count(e => e.Id == id) > 0;
        }
    }
}