using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace BeerCollection.Models
{
    public class GetJsonData
    {
        private static string GetJsonValue()
        {
            var path = HostingEnvironment.MapPath("/Database/BeersDB.json");
            string result = string.Empty;
            using (StreamReader r = new StreamReader(path))
            {
                result = r.ReadToEnd();
            }
            return result;

        }
        private static bool WriteJsonData(string json)
        {
            try
            {
                var path = HostingEnvironment.MapPath("/Database/BeersDB.json");
                System.IO.File.WriteAllText(path, json);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }    
        private static string SerilizeJson(List<BeerData> data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
        private static List<BeerData> DeSerilizeJson(string json)
        {
            return JsonConvert.DeserializeObject<List<BeerData>>(json);
        }

        public static List<BeerData> GetAllData(string name)
        {
            var json = GetJsonValue();
            List<BeerData> finalResult = DeSerilizeJson(json);
            var firsObjectsName = (string.IsNullOrEmpty(name)) ? finalResult : finalResult.FindAll(x => x.Name.Contains(name));

            return firsObjectsName;
        }
        public static bool PostNewBeer(BeerData data)
        {
            var allJsonData = GetJsonValue();
            List<BeerData> finalResult = DeSerilizeJson(allJsonData);
            finalResult.Add(data);
            var convertedJson = SerilizeJson(finalResult);
            return WriteJsonData(convertedJson);
        }
        /// <summary>
        /// this will update beer rating on basis of beer name
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateBeerRating(BeerData data)
        {
            List<BeerData> recordToUpdate = null;
            List<BeerData> totalData = null;
            
            if(data!=null && !string.IsNullOrEmpty(data.Name))
            {
                var allJsonData = GetJsonValue();
                totalData = DeSerilizeJson(allJsonData);
                recordToUpdate = (totalData!=null && totalData.Count>0)? totalData.FindAll(x => x.Name.ToLower() == data.Name.ToLower()):null;
                if(recordToUpdate!=null && recordToUpdate.Count > 0)
                {
                    foreach(var item in recordToUpdate)
                    {
                        item.Rating = data.Rating;
                    }
                    totalData.RemoveAll(x => x.Name.ToLower() == data.Name.ToLower());
                    totalData.AddRange(recordToUpdate);

                    return WriteJsonData(SerilizeJson(totalData));
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool DeleteBeer(string beerName)
        {
            List<BeerData> recordToDelete = null;
            List<BeerData> totalData = null;

            if (!string.IsNullOrEmpty(beerName))
            {
                var allJsonData = GetJsonValue();
                totalData = DeSerilizeJson(allJsonData);
                recordToDelete = (totalData != null && totalData.Count > 0) ? totalData.FindAll(x => x.Name.ToLower() == beerName.ToLower()) : null;
                if (recordToDelete != null && recordToDelete.Count > 0)
                {
                    totalData.RemoveAll(x => x.Name.ToLower() == beerName.ToLower());
                    return WriteJsonData(SerilizeJson(totalData));
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}