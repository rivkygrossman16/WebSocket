using System;
using System.Text.Json.Serialization;

namespace ReactWebSocket.Data
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Job { get; set; }
        public int? UserId { get; set; }
        public Status status { get; set; }
        public User? User { get; set; }
    }
}
