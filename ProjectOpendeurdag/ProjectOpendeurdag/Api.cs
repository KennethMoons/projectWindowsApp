using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag
{
    /// <summary>
    /// Helper for retrieving objects from web API
    /// </summary>
    class Api
    {
        /// <summary>
        /// API base URL
        /// </summary>
        public const string API_URL = "http://localhost:51399/api";

        /// <summary>
        /// Retrieve JSON from API
        /// </summary>
        /// <param name="name">Name of the controller</param>
        /// <returns>JSON formatted string</returns>
        public static async Task<string> GetJsonAsync(string name)
        {
            return await new HttpClient().GetStringAsync(String.Format("{0}/{1}", API_URL, name));
        }

        /// <summary>
        /// Retrieve object from API
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="name">Name of the controller</param>
        /// <returns>Deserialized object</returns>
        public static async Task<T> GetAsync<T>(string name)
        {
            return JsonConvert.DeserializeObject<T>(await GetJsonAsync(name));
        }
    }
}
