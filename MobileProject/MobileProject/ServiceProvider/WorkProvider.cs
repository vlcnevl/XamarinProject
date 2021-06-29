using MobileProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileProject.ServiceProvider
{
    public class WorkProvider
    {
        public string url { get; set; } = "http://192.168.1.42:45455/api/works/";


        public async Task<Result> AddWork(Work work)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(work);
                var response = await client.PostAsync(url + "add", new StringContent(json, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                Result workResult = JsonConvert.DeserializeObject<Result>(content);
                return workResult;

            }
        }


        public async Task<WorkDataResult> GetAllWork()
        {
            using (WebClient client = new WebClient())
            {
                var response =  client.DownloadString(url + "getall");
                WorkDataResult workDataResult = JsonConvert.DeserializeObject<WorkDataResult>(response);
                return workDataResult;
            }
        }
        public async Task<WorkDataResult> GetAllWorkByUserId(int userId)
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString(url + "getbyuserid?userId="+ userId);
                WorkDataResult workDataResult = JsonConvert.DeserializeObject<WorkDataResult>(response);
                return workDataResult;
            }
        }

        public async Task<Result> DeleteWork(Work work)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(work);
                var response = await client.PostAsync(url + "delete", new StringContent(json, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                Result workResult = JsonConvert.DeserializeObject<Result>(content);
                return workResult;

            }
        }

        public async Task<WorkDataResult> GetWorksByTittle(string tittle)
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString(url + "getbytittle?tittle=" + tittle);
                WorkDataResult workDataResult = JsonConvert.DeserializeObject<WorkDataResult>(response);
                return workDataResult;
            }
        }

    }
}
