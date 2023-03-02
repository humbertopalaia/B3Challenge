using B3Challenge.Domain.Dtos.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Business.Interfaces
{
    public interface ITaskBusiness
    {
        void Insert(Domain.Entities.Task entity, bool autoSave = true);
        void Update(Domain.Entities.Task entity, bool autoSave = true);
        void Delete(int id);
        Domain.Entities.Task GetById(int id);
        IList<Domain.Entities.Task> Filter(TaskFilterDto dto);
    }
}
