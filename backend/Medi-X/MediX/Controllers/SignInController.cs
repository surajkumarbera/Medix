using MediX.Context;
using MediX.Models;
using System.Linq;
using System.Web.Http;

namespace MediX.Controllers
{
    public class SignInController : ApiController
    {
        private readonly MediX_DbContext db = new MediX_DbContext();

        [HttpPost]
        [Route("api/signIn")]
        public IHttpActionResult SignInCustomer(SignIn userData)
        {
            if (!ModelState.IsValid || !userData.IsValid())
            {
                return BadRequest("Incorrect format");
            }

            var existingUser = db.Users.FirstOrDefault(user => user.Email == userData.Email);
            if (existingUser == null)
            {
                return NotFound();
            }
            else
            {
                if (Roles.IsAValidRole(existingUser.Role) && PasswordHashHelper.MatchHashedPassword(existingUser.Password, userData.Password))
                {
                    return Ok(new { existingUser, Authorization = JwtHelper.CreateJwtToken(existingUser.Email, existingUser.Role) });
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
            }
        }

    }
}

