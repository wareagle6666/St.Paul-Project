using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;
using Elmah;
using Newtonsoft.Json;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class StreamingController : Controller
    {
        // GET: Streaming
        public ActionResult Index()
        {
            var _http = new HttpClient();
            var token = "AIzaSyCCZeiRB-KNkpDFBkf_kGabfEDwFxSog7k";
            string url = $"https://www.googleapis.com/youtube/v3/search?part=snippet&channelId=UCAlsQJBDF-ZmSf6rZLLSHzw&type=video&eventType=live&key=AIzaSyCCZeiRB-KNkpDFBkf_kGabfEDwFxSog7k";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "accept", "application/json" }
                }
            };


            var result =  _http.SendAsync(request, CancellationToken.None);
            var responseStr = result.Result.Content.ReadAsStringAsync();

            var ResponseData =  JsonConvert.DeserializeObject<YouTubeResponse>(responseStr.Result);

            if (ResponseData.items.Count == 1)
            {
                ViewBag.IsLive = 1;
            }
            else
            {
                ViewBag.IsLive = 0;
            }

            return View();
        }

    }
}