using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using ReactWebSocket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactWebSocket.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly IHubContext<ChatHub> _context;
        private string _connectionString;

        public TaskController(IHubContext<ChatHub> context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("ConStr");
        }



        //[HttpPost]
        //[Route("addtask")]
        //public void AddTask(TaskItem taskItem)
        //{
        //    //taskItem.status == Status.Available;
        //    var repo = new TaskRepo(_connectionString);
        //    repo.AddTask(taskItem);
        //    var tasks= repo.GetTasks();
        //    _context.Clients.All.SendAsync("tasks", tasks);
        //}

        //[HttpGet]
        //[Route("gettasks")]
        //public List<TaskItem> GetTasks()
        //{
        //    var repo = new TaskRepo(_connectionString);
        //     return repo.GetTasks();
        //}
    }
}
