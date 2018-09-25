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
using System.Xml.Linq;

namespace rahareika_back.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly HttpClient client = new HttpClient();
        // GET api/values
        public string GetMonthlyUsdEurFrom1999ToNow()
        {
            
            var inputUrl = $"https://sdw-wsrest.ecb.europa.eu/service/data/EXR/M.USD.EUR.SP00.A";
            
            using (XmlReader reader = XmlReader.Create(inputUrl))
            {
                //reader.ReadStartElement("generic:Series");
                Dictionary<string, string> record = new Dictionary<string, string> { };

                string timestamp = "startvalue";
                string valueInComparison = "startvalue";
                string lastValue = "startvalue";
                bool valueHasUpdated = false;

                while (reader.Read())
                {
                    if (reader.LocalName == "ObsDimension")
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.LocalName != null || reader.LocalName != "")
                            {
                                if (reader.Value != null || reader.Value != "")
                                {
                                    timestamp = reader.Value;
                                }
                            }
                        }
                    }

                    if (reader.LocalName == "ObsValue")
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.LocalName != null || reader.LocalName != "")
                            {
                                if (reader.Value != null || reader.Value != "")
                                {
                                    valueInComparison = reader.Value;
                                    valueHasUpdated = true;
                                }
                            }
                        }
                    }

                    if (timestamp != "humppa" && valueInComparison != "hamppa" && timestamp != lastValue && valueHasUpdated)
                    {
                        record.Add(timestamp, valueInComparison);
                        lastValue = timestamp;
                        valueHasUpdated = false;
                    }

                }
                string paluuStringi = "Tästä lähtee: " + System.Environment.NewLine;
                foreach (KeyValuePair<string, string> entry in record)
                {
                    paluuStringi = paluuStringi + "Timestamp: " + entry.Key + " Value: " + entry.Value + System.Environment.NewLine;
                }
                return paluuStringi;
            }
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
