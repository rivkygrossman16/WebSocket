using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactWebSocket.Data
{
   public class TaskRepo
    {

        private readonly string _connectionString;

        public TaskRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTask(TaskItem task)
        {
            using var ctx = new WebSocketContext(_connectionString);
            ctx.TaskItems.Add(task);
            ctx.SaveChanges();
        }

        public List<TaskItem> GetTasks()
        {
            using var ctx = new WebSocketContext(_connectionString);
            return ctx.TaskItems.Include(t => t.User).ToList();
        }


        public void MarkAsDoingTask(TaskItem task)
        {

            using var ctx = new WebSocketContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"Update TaskItems SET Status={Status.TakenByOtherUser}, UserId={task.UserId} WHERE Id = {task.Id}");
            ctx.SaveChanges();
        }
        public void MarkAsDoneTask(int id)
        {

            using var ctx = new WebSocketContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"DELETE FROM TaskItems WHERE Id = {id}");
            ctx.SaveChanges();
        }


    }
}
