using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace rahareika_back.Controllers
{
    [RoutePrefix("rates")]
    public class RatesController : Controller
    {

        string url = "https://api.exchangeratesapi.io/latest";

        // GET: Rates/GetLatestRates
        public async Task<ActionResult> GetLatestRates()
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

        // GET: Rates/HistoricalRates/YYYYMMDD
        public async Task<ActionResult> HistoricalRates(int id)
        {
            string date = id.ToString();
            string year = date.Substring(0, 4);
            string day = date.Substring(4, 2);
            string month = date.Substring(6, 2);
            string urlDate = year + "-" + day + "-" + month;

            string newUrl = "https://api.exchangeratesapi.io/" + urlDate;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(newUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(newUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    return (Content(jsonData, "application/json"));
                }

                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Rates/CompareRates/YYYY-MM-DD/YYYY-MM-DD

        [Route("comparerates /{startDate}/{endDate }")]
        public async Task<ActionResult> CompareRates(string startDate, string endDate)
        {

            string newUrl = "https://api.exchangeratesapi.io/" + startDate;
            string newUrl2 = "https://api.exchangeratesapi.io/" + endDate;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(newUrl);
                client.BaseAddress = new System.Uri(newUrl2);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(newUrl);
                HttpResponseMessage response2 = await client.GetAsync(newUrl2);

                if (response.IsSuccessStatusCode && response2.IsSuccessStatusCode)
                {

                    string jsonData = await response.Content.ReadAsStringAsync();
                    string jsonData2 = await response2.Content.ReadAsStringAsync();

                    return Json(jsonData + jsonData2, JsonRequestBehavior.AllowGet);
                }

                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
