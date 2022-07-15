using MediX.Context;
using MediX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MediX.Controllers
{
    public class CategoriesController : ApiController
    {
        private MediX_DbContext db = new MediX_DbContext();

        [HttpGet]
        [Route("api/Categories")]
        public IHttpActionResult GetCategories()
        {
            List<Object> categoryList = new List<Object>();

            foreach (var category in db.Categories)
            {
                var imageUrl = ImageHandler.ImageCdnUrl + "/" + category.ImageUrl;
                categoryList.Add(new
                {
                    category.Id,
                    category.Name,
                    imageUrl
                });
            }
            return Ok(categoryList);
        }

        [HttpGet]
        [Route("api/Categories/{id}")]
        public IHttpActionResult GetCategories(int id)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return NotFound();
            }

            var imageUrl = ImageHandler.ImageCdnUrl + "/" + categories.ImageUrl;
            return Ok(new
            {
                categories.Id,
                categories.Name,
                imageUrl
            });
        }
        /*
        [HttpPut]
        [Route("api/Categories/{id}")]
        public IHttpActionResult PutCategories(int id, Categories categories)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized || role != Roles.Admin) return BadRequest("Unauthorized");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categories.Id)
            {
                return BadRequest();
            }

            try
            {
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(categories);
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!CategoriesExists(id))
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
        [Route("api/Categories")]
        public IHttpActionResult PostCategories()
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized || role != Roles.Admin) return BadRequest("Unauthorized");

            Categories categories = new Categories();
            categories.Name = HttpContext.Current.Request.Form["Name"];
            categories.ImageUrl = ImageHandler.PostToCdn(Request);

            db.Categories.Add(categories);
            db.SaveChanges();

            var imageUrl = ImageHandler.ImageCdnUrl + "/" + categories.ImageUrl;
            return Ok(new
            {
                categories.Id,
                categories.Name,
                imageUrl
            });
        }

        [HttpDelete]
        [Route("api/Categories/{id}")]
        public IHttpActionResult DeleteCategories(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized || role != Roles.Admin) return BadRequest("Unauthorized");

            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return NotFound();
            }

            db.Categories.Remove(categories);
            db.SaveChanges();

            return Ok(categories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriesExists(int id)
        {
            return (db.Categories.Count(e => e.Id == id) > 0);
        }
    }
}