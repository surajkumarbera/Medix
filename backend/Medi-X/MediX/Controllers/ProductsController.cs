using MediX.Context;
using MediX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MediX.Controllers
{
    public class ProductsController : ApiController
    {
        private MediX_DbContext db = new MediX_DbContext();

        [HttpGet]
        [Route("api/Products")]
        public IHttpActionResult GetProducts()
        {
            var allProducts = db.Products;
            List<Object> resP = new List<Object>();

            foreach (Products product in allProducts)
            {
                var imageUrl = ImageHandler.ImageCdnUrl + "/" + product.ImageUrl;
                resP.Add(new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Vendor = product.Vendor,
                    Descriptions = product.Descriptions,
                    CategoriesId = product.CategoriesId,
                    Category = db.Categories.Find(product.CategoriesId).Name,
                    SubCategoriesId = product.SubCategoriesId,
                    SubCategories = db.SubCategories.Find(product.SubCategoriesId).Name,
                    ImageUrl = imageUrl
                });

            }
            return Ok(resP);
        }

        [HttpGet]
        [Route("api/Products/{id}")]
        public IHttpActionResult GetProducts(int id)
        {
            Products product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            var imageUrl = ImageHandler.ImageCdnUrl + "/" + product.ImageUrl;

            return Ok(new
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Vendor = product.Vendor,
                Descriptions = product.Descriptions,
                CategoriesId = product.CategoriesId,
                Category = db.Categories.Find(product.CategoriesId).Name,
                SubCategoriesId = product.SubCategoriesId,
                SubCategories = db.SubCategories.Find(product.SubCategoriesId).Name,
                ImageUrl = imageUrl
            });
        }

        [HttpGet]
        [Route("api/Products/Categories/{id}")]
        public IHttpActionResult GetProductsByCategory(int id)
        {
            var allProducts = db.Products.Where(p => p.CategoriesId == id);
            List<Object> resP = new List<Object>();

            foreach (Products product in allProducts)
            {
                var imageUrl = ImageHandler.ImageCdnUrl + "/" + product.ImageUrl;
                resP.Add(new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Vendor = product.Vendor,
                    Descriptions = product.Descriptions,
                    CategoriesId = product.CategoriesId,
                    Category = db.Categories.Find(product.CategoriesId).Name,
                    SubCategoriesId = product.SubCategoriesId,
                    SubCategories = db.SubCategories.Find(product.SubCategoriesId).Name,
                    ImageUrl = imageUrl
                });

            }
            return Ok(resP);
        }

        [HttpGet]
        [Route("api/Products/SubCategories/{id}")]
        public IHttpActionResult GetProductsBySubCategory(int id)
        {
            var allProducts = db.Products.Where(p => p.SubCategoriesId == id);
            List<Object> resP = new List<Object>();

            foreach (Products product in allProducts)
            {
                var imageUrl = ImageHandler.ImageCdnUrl + "/" + product.ImageUrl;
                resP.Add(new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Vendor = product.Vendor,
                    Descriptions = product.Descriptions,
                    CategoriesId = product.CategoriesId,
                    Category = db.Categories.Find(product.CategoriesId).Name,
                    SubCategoriesId = product.SubCategoriesId,
                    SubCategories = db.SubCategories.Find(product.SubCategoriesId).Name,
                    ImageUrl = imageUrl
                });

            }
            return Ok(resP);
        }

        /*
        [HttpPut]
        [Route("api/Products/{id}")]
        public IHttpActionResult PutProducts(int id, Products products)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized || role != Roles.Admin) return BadRequest("Unauthorized");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (id != products.Id)
                {
                    return BadRequest();
                }

                Products exsistingProduct = db.Products.Find(id);
                if (exsistingProduct == null)
                {
                    return NotFound();
                }

                exsistingProduct.Name = products.Name;
                exsistingProduct.Price = products.Price;
                exsistingProduct.Quantity = products.Quantity;
                exsistingProduct.CategoriesId = products.CategoriesId;
                exsistingProduct.SubCategoriesId = products.SubCategoriesId;

                db.SaveChanges();


                return Ok(new
                {
                    exsistingProduct.Id,
                    exsistingProduct.Name,
                    exsistingProduct.Price,
                    exsistingProduct.Quantity,
                    exsistingProduct.CategoriesId,
                    exsistingProduct.SubCategoriesId,
                    products.ImageUrl
                });
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ProductsExists(id))
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
        [Route("api/Products")]
        public IHttpActionResult PostProducts()
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized || role != Roles.Admin) return BadRequest("Unauthorized");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                Products product = new Products
                {
                    Name = HttpContext.Current.Request.Form["Name"],
                    Price = float.Parse(HttpContext.Current.Request.Form["Price"]),
                    Quantity = int.Parse(HttpContext.Current.Request.Form["Quantity"]),
                    Vendor = HttpContext.Current.Request.Form["Vendor"],
                    Descriptions = HttpContext.Current.Request.Form["Descriptions"],
                    CategoriesId = int.Parse(HttpContext.Current.Request.Form["CategoriesId"]),
                    SubCategoriesId = int.Parse(HttpContext.Current.Request.Form["SubCategoriesId"]),
                    ImageUrl = ImageHandler.PostToCdn(Request)
                };


                db.Products.Add(product);
                db.SaveChanges();

                var imageUrl = ImageHandler.ImageCdnUrl + "/" + product.ImageUrl;
                return Ok(new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Vendor = product.Vendor,
                    Descriptions = product.Descriptions,
                    CategoriesId = product.CategoriesId,
                    Category = db.Categories.Find(product.CategoriesId).Name,
                    SubCategoriesId = product.SubCategoriesId,
                    SubCategories = db.SubCategories.Find(product.SubCategoriesId).Name,
                    ImageUrl = imageUrl
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("api/Products/{id}")]
        public IHttpActionResult DeleteProducts(int id)
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);
            if (!isAuthorized || role != Roles.Admin) return BadRequest("Unauthorized");

            Products product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();


            var imageUrl = ImageHandler.ImageCdnUrl + "/" + product.ImageUrl;
            return Ok(new
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Vendor = product.Vendor,
                Descriptions = product.Descriptions,
                CategoriesId = product.CategoriesId,
                Category = db.Categories.Find(product.CategoriesId).Name,
                SubCategoriesId = product.SubCategoriesId,
                SubCategories = db.SubCategories.Find(product.SubCategoriesId).Name,
                ImageUrl = imageUrl
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}