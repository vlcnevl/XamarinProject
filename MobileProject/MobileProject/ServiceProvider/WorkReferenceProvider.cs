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
    public class WorkReferenceProvider
    {
        public string url { get; set; } = "http://192.168.1.42:45455/api/workreferences/";

        public async Task<Result> AddWorkReferences(WorkReference workReference)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(workReference);
                var response = await client.PostAsync(url + "add", new StringContent(json, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                Result workReferenceResult = JsonConvert.DeserializeObject<Result>(content);
                return workReferenceResult;
            }
        }

        public async Task<WorkReferenceDataResult> GetAllWorkReferenceByUserId(int userId)
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString(url + "getbyuserid?userId=" + userId);
                WorkReferenceDataResult workReferenceDataResult = JsonConvert.DeserializeObject<WorkReferenceDataResult>(response);
                return workReferenceDataResult;
            }
        }

        public async Task<WorkDetailDataResult> GetUserWithWorkId(int workId)
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString(url + "getworkdetails?workId=" + workId);
                WorkDetailDataResult workDataResults = JsonConvert.DeserializeObject<WorkDetailDataResult>(response);
                return workDataResults;
            }
        }

        public async Task<WorkDataResult> GetMyApplications(int userId)
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString(url + "getuserapplication?userId=" + userId);
                WorkDataResult workDataResults = JsonConvert.DeserializeObject<WorkDataResult>(response);
                return workDataResults;
            }

        }
    }
}
