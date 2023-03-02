using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace B3Challenge.Domain.Dtos.Task
{
    public class TaskInsertDto
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int TaskStatusId { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
