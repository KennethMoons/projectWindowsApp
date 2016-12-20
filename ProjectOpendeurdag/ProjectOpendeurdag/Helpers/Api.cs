using Newtonsoft.Json;
using ProjectOpendeurdag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Storage;
using System.Diagnostics;
using ProjectOpendeurdag.Helpers;

namespace ProjectOpendeurdag.Helpers
{
    /// <summary>
    /// Helper for retrieving objects from web API
    /// </summary>
    class Api
    {
        /// <summary>
        /// API base URL
        /// </summary>
        public const string API_URL = "http://projectwindowsapp.azurewebsites.net//api";

        public const string API_KEY = "mGI4VHw6fmhsPSG44oa8yhB9xlZGKyps";

        /// <summary>
        /// Dictionary to map models to correct API URI
        /// </summary>
        public static Dictionary<Type, string> URI = new Dictionary<Type, string>
        {
            { typeof(Campus), "Campus" },
            { typeof(Gebruiker), "Gebruikers" },
            { typeof(Infomoment), "Infomoments" },
            { typeof(Newsitem), "Newsitems" },
            { typeof(Opleiding), "Opleidings" }
        };


        /// <summary>
        /// DELETE object from API
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="id">ID</param>
        /// <returns>Deserialized object</returns>
        public static async Task<HttpResponseMessage> DeleteAsync<T>(int id)
        {
            string uri = GetUri<T>(id);
            Debug.WriteLine("DELETE {0}", (object)uri);
            return await GetHttpClient().DeleteAsync(uri);
        }

        /// <summary>
        /// GET object from API
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <returns>Deserialized object</returns>
        public static async Task<T> GetAsync<T>()
        {
            string uri = GetUri<T>();
            Debug.WriteLine("GET {0}", (object) uri);
            return JsonConvert.DeserializeObject<T>(await GetHttpClient().GetStringAsync(uri));
        }

        /// <summary>
        /// GET object from API
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="id">ID</param>
        /// <returns>Deserialized object</returns>
        public static async Task<T> GetAsync<T>(object id)
        {
            string uri = GetUri<T>(id);
            Debug.WriteLine("GET {0}", (object) uri);
            return JsonConvert.DeserializeObject<T>(await GetHttpClient().GetStringAsync(uri));
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
        private static string GetUri<T>(object id)
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
            Debug.WriteLine("POST {0} -> {1}", uri, json);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GetHttpClient().PostAsync(uri, content);
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
            Debug.WriteLine("PUT {0} -> {1}", uri, json);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GetHttpClient().PutAsync(uri, content);
        }

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();

            // Set API key
            client.DefaultRequestHeaders.Add("API_KEY", API_KEY);

            // Set authorization if logged in
            if (Settings.IsGebruikerLoggedIn())
            {
                var loginCredential = GetCredentialFromLocker();

                if (loginCredential != null)
                {
                    var username = loginCredential.UserName;
                    var password = loginCredential.Password;
                    var auth = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(String.Format("{0}:{1}", username, password)));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", auth);
                }
            }

            return client;
        }

        private static PasswordCredential GetCredentialFromLocker()
        {
            PasswordCredential credential = null;

            var vault = new PasswordVault();
            try
            {
                var credentials = vault.FindAllByResource(API_KEY);

                if (credentials.Count > 0)
                {
                    credential = credentials[0];
                    credential.RetrievePassword();
                }
            }
            catch (Exception)
            {
                // No credentials found
            }

            return credential;
        }

        public static Gebruiker Login()
        {
            var credentials = GetCredentialFromLocker();

            if (credentials != null)
            {
                return Login(credentials.UserName, credentials.Password).Result;
            }

            return null;
        }

        public static void Logout()
        {
            Debug.WriteLine("LOGOUT");

            Settings.SetCurrentGebruiker(null);

            RemoveCredentials();
        }

        public static async Task<Gebruiker> Login(string email, string password)
        {
            Debug.WriteLine("LOGIN {0}", (object) email);

            HttpClient client = new HttpClient();

            // Set API key
            client.DefaultRequestHeaders.Add("API_KEY", API_KEY);

            // Set authorization
            var auth = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(String.Format("{0}:{1}", email, password)));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", auth);

            Gebruiker user = null;

            try
            {
                user = JsonConvert.DeserializeObject<Gebruiker>(await client.GetStringAsync(GetUri<Gebruiker>("current")));
            }
            catch (Exception)
            {
                // Login failed
            }

            if (user != null)
            {
                SetCredentials(email, password);
            }

            Settings.SetCurrentGebruiker(user);

            return user;
        }

        private static void SetCredentials(string email, string password)
        {
            RemoveCredentials();

            var vault = new PasswordVault();

            // Add new credentials
            vault.Add(new PasswordCredential(API_KEY, email, password));
        }

        private static void RemoveCredentials()
        {
            var vault = new PasswordVault();

            // Remove previous credentials (if any)
            try
            {
                vault.FindAllByResource(API_KEY).ToList().ForEach(c => vault.Remove(c));
            }
            catch
            {
                // No credentials
            }
        }
    }
}
