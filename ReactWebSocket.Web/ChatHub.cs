using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using ReactWebSocket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactWebSocket.Web
{
    public class ChatHub:Hub
    {
        private string _connectionString;

        public ChatHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [Authorize]
        public void GetTasks()
        {
            var repo = new TaskRepo(_connectionString);
            var tasks = repo.GetTasks();
            Clients.All.SendAsync("getTasks", tasks);
        }
        [Authorize]
        public void AddTask(TaskItem task)
        {
            task.status = 0;
            var repo = new TaskRepo(_connectionString);
            repo.AddTask(task);
            Clients.All.SendAsync("addTask", repo.GetTasks());
        }
        [Authorize]
        public void MarkAsDoing(TaskItem task)
        {
            var userRepo = new UserRepository(_connectionString);
            task.UserId = userRepo.GetByEmail(Context.User.Identity.Name).Id;
            var repo = new TaskRepo(_connectionString);
            repo.MarkAsDoingTask(task);
            Clients.All.SendAsync("addTask", repo.GetTasks());
        }
        [Authorize]
        public void MarkAsDone(TaskItem task)
        {
            var userRepo = new UserRepository(_connectionString);
            var repo = new TaskRepo(_connectionString);
            task.UserId = userRepo.GetByEmail(Context.User.Identity.Name).Id;
            repo.MarkAsDoneTask(task.Id);
            Clients.All.SendAsync("addTask", repo.GetTasks());
        }
    }
}
