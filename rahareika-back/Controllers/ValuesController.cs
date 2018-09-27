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
using System.Web.Mvc;
using rahareika_back.Models;
using Newtonsoft;
using System.Web.Http.Cors;

namespace rahareika_back.Controllers
{
    [EnableCors(origins: "http://localhost", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        private static readonly HttpClient client = new HttpClient();
        // GET api/values/{comparingcurrency}

        public string GetDailyEurCurrencyOfChoiceFromLast30Days(string basecurrency)
        {

            var inputUrl = $"https://sdw-wsrest.ecb.europa.eu/service/data/EXR/D.{basecurrency}.EUR.SP00.A?startPeriod="
                + DateTime.Now.AddDays(-31).ToString("yyyy-MM-dd")
                + "&endPeriod=" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            try
            {
                using (XmlReader reader = XmlReader.Create(inputUrl))
                {
                    //reader.ReadStartElement("generic:Series");
                    Dictionary<string, string> record = new Dictionary<string, string> { };

                    // variables for checking parse status 
                    string timestamp = "startvalue";
                    string valueInComparison = "startvalue";
                    string lastValue = "startvalue";
                    bool valueHasUpdated = false;

                    while (reader.Read())
                    {
                        //parse timestamps (ObsDimension) from currency observation timepoints and currency values in comparison (ObsValue) - "Obs" in ECB's SDMX hold these following values
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
                        // collect values to dictionary, when the reader has corresponding timestamp and currency comparison values
                        if (timestamp != "startvalue" && valueInComparison != "startvalue" && timestamp != lastValue && valueHasUpdated)
                        {
                            record.Add(timestamp, valueInComparison);
                            lastValue = timestamp;
                            valueHasUpdated = false;
                        }

                    }
                    EurToSmth values = new EurToSmth() { ComparingCurrency = basecurrency, TimeValuePairs = record };
                    
                    return JsonConvert.SerializeObject(values);
                }
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
        //// GET api/values/
        //public string GetDailyEurToAllcurrenciesFromLast30Days()
        //{

        //    var inputUrl = $"https://sdw-wsrest.ecb.europa.eu/service/data/EXR/D..EUR.SP00.A?startPeriod="
        //        + DateTime.Now.AddDays(-31).ToString("yyyy-MM-dd")
        //        + "&endPeriod=" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        //    try
        //    {
        //        using (XmlReader reader = XmlReader.Create(inputUrl))
        //        {
        //            //reader.ReadStartElement("generic:Series");
        //            Dictionary<string, string> record = new Dictionary<string, string> { };

        //            // variables for checking parse status 
        //            string timestamp = "startvalue";
        //            string valueInComparison = "startvalue";
        //            string lastValue = "startvalue";
        //            bool valueHasUpdated = false;

        //            while (reader.Read())
        //            {
        //                //parse timestamps (ObsDimension) from currency observation timepoints and currency values in comparison (ObsValue) - "Obs" in ECB's SDMX hold these following values
        //                if (reader.LocalName == "ObsDimension")
        //                {
        //                    while (reader.MoveToNextAttribute())
        //                    {
        //                        if (reader.LocalName != null || reader.LocalName != "")
        //                        {
        //                            if (reader.Value != null || reader.Value != "")
        //                            {
        //                                timestamp = reader.Value;
        //                            }
        //                        }
        //                    }
        //                }

        //                if (reader.LocalName == "ObsValue")
        //                {
        //                    while (reader.MoveToNextAttribute())
        //                    {
        //                        if (reader.LocalName != null || reader.LocalName != "")
        //                        {
        //                            if (reader.Value != null || reader.Value != "")
        //                            {
        //                                valueInComparison = reader.Value;
        //                                valueHasUpdated = true;
        //                            }
        //                        }
        //                    }
        //                }
        //                // collect values to dictionary, when the reader has corresponding timestamp and currency comparison values
        //                if (timestamp != "startvalue" && valueInComparison != "startvalue" && timestamp != lastValue && valueHasUpdated)
        //                {
        //                    record.Add(timestamp, valueInComparison);
        //                    lastValue = timestamp;
        //                    valueHasUpdated = false;
        //                }

        //            }
        //            string paluuStringi = "Tästä lähtee: " + System.Environment.NewLine;
        //            foreach (KeyValuePair<string, string> entry in record)
        //            {
        //                paluuStringi = paluuStringi + "Timestamp: " + entry.Key + " Value: " + entry.Value + System.Environment.NewLine;
        //            }
        //            return paluuStringi;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }
        //}

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
