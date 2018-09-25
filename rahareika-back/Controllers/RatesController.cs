using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace rahareika_back.Controllers
{

    public class RatesController : Controller
    {

        //HttpClient httpClient = new HttpClient();
        string url = "https://api.exchangeratesapi.io/latest";

        public async Task<ActionResult> GetRates()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(url); 
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(url); 
                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    return (Content(jsonData, "application/json"));
                }

                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
