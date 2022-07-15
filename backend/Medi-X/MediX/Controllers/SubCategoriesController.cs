using MediX.Context;
using MediX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MediX.Controllers
{
    public class SubCategoriesController : ApiController
    {
        private MediX_DbContext db = new MediX_DbContext();

        [HttpGet]
        [Route("api/Subcategories")]
        public IHttpActionResult GetSubCategories()
        {
            List<Object> subCategoryList = new List<Object>();

            foreach (var subCategory in db.SubCategories)
            {
                var imageUrl = ImageHandler.ImageCdnUrl + "/" + subCategory.ImageUrl;
                subCategoryList.Add(new
                {
                    subCategory.Id,
                    subCategory.Name,
                    imageUrl
                });
            }

            return Ok(subCategoryList);
        }

        [HttpGet]
        [Route("api/SubCategories/{id}")]
        public IHttpActionResult GetSubCategories(int id)
        {
            SubCategories subCategories = db.SubCategories.Find(id);
            if (subCategories == null)
            {
                return NotFound();
            }

            var imageUrl = ImageHandler.ImageCdnUrl + "/" + subCategories.ImageUrl;
            return Ok(new
            {
                subCategories.Id,
                subCategories.Name,
                imageUrl
            });
        }
        /*
        [HttpPut]
        [Route("api/SubCategories/{id}")]
        public IHttpActionResult PutSubCategories(int id, SubCategories subCategories)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized || role != Roles.Admin) return BadRequest("Unauthorized");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (id != subCategories.Id)
                {
                    return BadRequest();
                }

                db.Entry(subCategories).State = EntityState.Modified;
                db.SaveChanges();

                return Ok(subCategories);
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!SubCategoriesExists(id))
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
        [Route("api/SubCategories")]
        public IHttpActionResult PostSubCategories()
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized || role != Roles.Admin) return BadRequest("Unauthorized");

            try
            {
                SubCategories subCategories = new SubCategories();
                subCategories.Name = HttpContext.Current.Request.Form["Name"];
                subCategories.ImageUrl = ImageHandler.PostToCdn(Request);

                db.SubCategories.Add(subCategories);
                db.SaveChanges();

                var imageUrl = ImageHandler.ImageCdnUrl + "/" + subCategories.ImageUrl;
                return Ok(new
                {
                    subCategories.Id,
                    subCategories.Name,
                    imageUrl
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete]
        [Route("api/SubCategories/{id}")]
        public IHttpActionResult DeleteSubCategories(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized || role != Roles.Admin) return BadRequest("Unauthorized");

            SubCategories subCategories = db.SubCategories.Find(id);
            if (subCategories == null)
            {
                return NotFound();
            }

            db.SubCategories.Remove(subCategories);
            db.SaveChanges();

            return Ok(subCategories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubCategoriesExists(int id)
        {
            return db.SubCategories.Count(e => e.Id == id) > 0;
        }
    }
}