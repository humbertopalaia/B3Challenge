using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Domain.Dtos.Task
{
    public class TaskFilterDto
    {
        public TaskFilterDto(int? id, string? description, int? taskStatusId)
        {
            Id = id;
            Description = description;
            TaskStatusId = taskStatusId;
        }

        public int? Id { get; set; }
        public string? Description { get; set; }
        public int? TaskStatusId { get; set; }
    }
}
