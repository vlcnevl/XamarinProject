using MobileProject.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileProject.Models
{ 
    
    public class GetUserProfile 
    {
        public static User GetUser()
        {
            List<Task> taskList = new List<Task>();
            UserProvider userProvider = new UserProvider();
            Task<UserDataResults> userTask = Task<UserDataResults>.Run(() => userProvider.GetByEmail(App.Email).Result);
            taskList.Add(userTask);
            Task.WaitAll(taskList.ToArray());

            if (userTask.Status == TaskStatus.RanToCompletion)
            {
                var result = userTask.Result;

                if (result.Success)
                {
                    return result.Data;

                }
            } return null;
        }

        public static string GetUserById(int userId)
        {
            List<Task> taskList = new List<Task>();
            UserProvider userProvider = new UserProvider();
            Task<UserDataResults> userTask = Task<UserDataResults>.Run(() => userProvider.GetByUserId(userId).Result);
            taskList.Add(userTask);
            Task.WaitAll(taskList.ToArray());

            if (userTask.Status == TaskStatus.RanToCompletion)
            {
                var result = userTask.Result;

                if (result.Success)
                {
                    return result.Data.Email;

                }
            }
            return null;
        }



    }
}
