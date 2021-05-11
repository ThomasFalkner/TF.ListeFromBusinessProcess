using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TF.ListeFromBusinessProcess.Dto;

namespace TF.ListeFromBusinessProcess
{
    public static class RkiData
    {

        const string url = @"https://api.corona-zahlen.org/states/";


        public static List<RkiDataElement> GetData()
        {
            var result = new List<RkiDataElement>();

            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest == null)
            {
                return result;
            }

            webRequest.ContentType = "application/json";
            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var rkiJson = sr.ReadToEnd();

                    JObject dataList = JObject.Parse(rkiJson);
                    IEnumerable<JToken> jsonResults = dataList.SelectTokens("$['data'].*");
                    IList<RkiDataElement> rkiDataResults = new List<RkiDataElement>();
                    foreach (JToken item in jsonResults)
                    {
                        result.Add(item.ToObject<RkiDataElement>());
                    }

                }
            }
            return result;

        }

    }

}

