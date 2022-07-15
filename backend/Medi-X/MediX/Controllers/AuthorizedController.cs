using MediX.Models;
using System.Web.Http;

namespace MediX.Controllers
{
    public class AuthorizedController : ApiController
    {
        [HttpGet]
        [Route("api/authorized")]
        public IHttpActionResult Authorized()
        {
            bool isAuthorized = JwtHelper.AuthenticationRequest(this.Request, out string role);

            if (isAuthorized)
            {
                return Ok(role);
            }
            else
            {
                return BadRequest("UnAuthorized");
            }
        }
    }
}
