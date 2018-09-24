using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using Newtonsoft.Json;
using System.IO;


namespace rahareika_back.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly HttpClient client = new HttpClient();
        // GET api/values
        public async Task Get()
        {
            XmlDocument responseFromBankXml = new XmlDocument();
            var response = await client.GetAsync($"https://sdw-wsrest.ecb.europa.eu/service/data/EXR/M.USD.EUR.SP00.A");
            responseFromBankXml.LoadXml(response.ToString());
            string responseFromBankJson = JsonConvert.SerializeXmlNode(responseFromBankXml);
            File.WriteAllText(@"C:\Temp\pankkijson.json", responseFromBankJson);
            return;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
