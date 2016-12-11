using Newtonsoft.Json;
using ProjectOpendeurdag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
        public const string API_URL = "http://projectwindowsapp.azurewebsites.net/api";

        /// <summary>
        /// Dictionary to map models to correct API URI
        /// </summary>
        public static Dictionary<Type, string> URI = new Dictionary<Type, string>
        {
            { typeof(Campus), "Campus" },
            { typeof(Gebruiker), "Gebruikers" },
            { typeof(Infomoment), "Infomoments" },
            { typeof(Newsitem), "Newsitems" },
            { typeof(Opleiding), "Opleidings" },
            { typeof(VoorkeurCampus), "VoorkeurCampus" },
            { typeof(VoorkeurOpleiding), "VoorkeurOpleidings" }
        };

        private static HttpClient http = new HttpClient();


        /// <summary>
        /// DELETE object from API
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="id">ID</param>
        /// <returns>Deserialized object</returns>
        public static async Task<HttpResponseMessage> DeleteAsync<T>(int id)
        {
            return await http.DeleteAsync(GetUri<T>(id));
        }

        /// <summary>
        /// GET object from API
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <returns>Deserialized object</returns>
        public static async Task<T> GetAsync<T>()
        {
            return await GetAsync<T>(GetUri<T>());
        }

        /// <summary>
        /// GET object from API
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="id">ID</param>
        /// <returns>Deserialized object</returns>
        public static async Task<T> GetAsync<T>(int id)
        {
            return await GetAsync<T>(GetUri<T>(id));
        }

        /// <summary>
        /// GET object from API
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="uri">URI</param>
        /// <returns>Deserialized object</returns>
        public static async Task<T> GetAsync<T>(string uri)
        {
            return JsonConvert.DeserializeObject<T>(await http.GetStringAsync(uri));
        }

        /// <summary>
        /// Get URI for type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>URI</returns>
        private static string GetUri<T>()
        {
            Type type = typeof(T);

            // Get generic type is T is List, ...
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                type = type.GetGenericArguments().Single();
            }

            return String.Format("{0}/{1}", API_URL, URI[type]);
        }

        /// <summary>
        /// Get URI for type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="id">ID</param>
        /// <returns>URI</returns>
        private static string GetUri<T>(int id)
        {
            return String.Format("{0}/{1}", GetUri<T>(), id);
        }

        /// <summary>
        /// POST object to API
        /// </summary>
        /// <param name="obj">Object to post</param>
        /// <returns>Http response</returns>
        public static async Task<HttpResponseMessage> PostAsync<T>(T obj)
        {
            return await PostAsync(GetUri<T>(), obj);
        }

        /// <summary>
        /// POST object to API
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="obj">Object to post</param>
        /// <returns>Http response</returns>
        private static async Task<HttpResponseMessage> PostAsync(string uri, object obj)
        {
            return await PostJsonAsync(uri, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// POST JSON to API
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="json">JSON formatted string</param>
        /// <returns>Http response</returns>
        private static async Task<HttpResponseMessage> PostJsonAsync(string uri, string json)
        {
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await http.PostAsync(uri, content);
        }

        /// <summary>
        /// PUT object to API
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="obj">Object to put</param>
        /// <returns>Http response</returns>
        public static async Task<HttpResponseMessage> PutAsync<T>(int id, T obj)
        {
            return await PutAsync(GetUri<T>(id), obj);
        }

        /// <summary>
        /// PUT object to API
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="obj">Object to put</param>
        /// <returns>Http response</returns>
        private static async Task<HttpResponseMessage> PutAsync(string uri, object obj)
        {
            return await PutJsonAsync(uri, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// PUT JSON to API
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="json">JSON formatted string</param>
        /// <returns>Http response</returns>
        private static async Task<HttpResponseMessage> PutJsonAsync(string uri, string json)
        {
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await http.PutAsync(uri, content);
        }
    }
}
