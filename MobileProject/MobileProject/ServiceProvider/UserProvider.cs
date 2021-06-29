using MobileProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileProject.ServiceProvider
{
   public class UserProvider
    {
        public string url { get; set; } = "http://192.168.1.42:45455/api/users/";

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
        public async Task<UserDataResults> GetByEmail(string email)
        {
            using (HttpClient client = await GetClient())
            {
                string json = JsonConvert.SerializeObject(email);
                var result = await client.GetStringAsync(url + "getbyemail?email="+email);
                UserDataResults userResult = JsonConvert.DeserializeObject<UserDataResults>(result);
                return userResult;
            }
        }

        public async Task<Result> UpdateUser(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(user);
                var response = await client.PutAsync(url + "update", new StringContent(json, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                Result updateResult = JsonConvert.DeserializeObject<Result>(content);
                return updateResult;

            }
        }

        public async Task<UserDataResults> GetByUserId(int userId)
        {
            using (HttpClient client = await GetClient())
            {
                string json = JsonConvert.SerializeObject(userId);
                var result = await client.GetStringAsync(url + "getbyid?id=" +userId);
                UserDataResults userResult = JsonConvert.DeserializeObject<UserDataResults>(result);
                return userResult;
            }
        }


    }
}
