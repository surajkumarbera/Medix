using MediX.Context;
using MediX.Models;
using System.Linq;
using System.Web.Http;

namespace MediX.Controllers
{
    public class SignUpController : ApiController
    {
        private readonly MediX_DbContext db = new MediX_DbContext();


        [HttpPost]
        [Route("api/signUp")]
        public IHttpActionResult CustomerSignUp(SignUp data)
        {
            User newUserData = new User()
            {
                Name = data.Name,
                Email = data.Email,
                MobileNo = data.MobileNo,
                Password = data.Password,
                Pincode = data.Pincode,
                Address = data.Address,
                Role = Roles.Customer
            };

            if (!ModelState.IsValid || !newUserData.IsUserValid())
            {
                return BadRequest("Incorrect format");
            }

            User existingUser = db.Users.FirstOrDefault(user => user.Email == newUserData.Email);
            if (existingUser == null)
            {
                newUserData.Password = PasswordHashHelper.HashPassword(newUserData.Password);
                db.Users.Add(newUserData);
                db.SaveChanges();

                return Ok(newUserData);
            }
            else
            {
                return BadRequest("User already exist. Try with different Email Id.");
            }

        }

    }
}
