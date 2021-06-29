using Entities.DTOs;
using MobileProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileProject.ServiceProvider
{
   public class AuthProvider
    {
        public string token;
   
        //http kullan https sertifika hatası
        public string url { get; set; } = "http://192.168.1.42:45455/api/auth/";

        public async Task<LoginRegisterResult> Register(UserForRegisterDto userForRegisterDto)
        {
            using (HttpClient client = new HttpClient()) 
            {
                string json = JsonConvert.SerializeObject(userForRegisterDto);
                var response = await client.PostAsync(url + "register", new StringContent(json, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();                
                LoginRegisterResult loginRegisterResult= JsonConvert.DeserializeObject<LoginRegisterResult>(content);
                return loginRegisterResult;
            }
        }
        public async Task<LoginRegisterResult> Login(UserForLoginDto userForLoginDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(userForLoginDto);
                var response = await client.PostAsync(url + "login", new StringContent(json, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                LoginRegisterResult loginRegisterResult = JsonConvert.DeserializeObject<LoginRegisterResult>(content);
                return loginRegisterResult;
            }
        }

    }
}
