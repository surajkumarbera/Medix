using System.Web.Http;

namespace MediX.Controllers
{
    public class ImageController : ApiController
    {
        /*
        [HttpPost]
        [Route("api/image")]
        public HttpResponseMessage Post()
        {
            using (var client = new HttpClient())
            {
                //string url = new Uri("http://127.0.0.1:80/api/image").ToString();     //local link
                string url = new Uri("https://medix-image-cdn.herokuapp.com/api/image").ToString();     //cloud link

                var postTask = client.PostAsync(url, Request.Content);
                postTask.Wait();
                var response = postTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    result.Wait();
                    var msg = result.Result;
                    msg = msg.Substring(1, (msg.Length - 2));
                    return Request.CreateResponse(HttpStatusCode.OK, msg);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        */
    }
}
