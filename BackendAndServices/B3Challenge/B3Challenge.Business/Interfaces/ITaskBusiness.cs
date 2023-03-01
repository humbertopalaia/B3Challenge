using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Business.Interfaces
{
    public interface ITaskBusiness
    {
        OperationResult Insert(Domain.Entities.Task entity, bool autoSave = true);
        OperationResult Update(Domain.Entities.Task entity, bool autoSave = true);
        OperationResult Delete(int id);
        Domain.Entities.Task GetById(int id);
        List<Domain.Entities.Task> GetAll();
    }
}
