using System;
using System.Net.Http;

namespace MediX.Models
{
    public static class ImageHandler
    {
        public static readonly string ImageCdnUrl = new Uri("https://medix-image-cdn.herokuapp.com/api/image").ToString();
        public static string PostToCdn(HttpRequestMessage Request)
        {
            using (var client = new HttpClient())
            {
                //string ImageCdnUrl = new Uri("http://127.0.0.1:80/api/image").ToString(); // loacl testing

                var postTask = client.PostAsync(ImageCdnUrl, Request.Content);
                postTask.Wait();

                var response = postTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    result.Wait();
                    var msg = result.Result;
                    msg = msg.Substring(1, (msg.Length - 2));
                    return msg;
                }
            }
            return null;
        }
    }
}