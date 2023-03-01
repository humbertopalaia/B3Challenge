using B3ChallengeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3ChallengeBusiness.Interfaces
{
    public interface ITaskBusiness
    {
        OperationResult Insert(B3ChallengeDomain.Task entity, bool autoSave = true);
        OperationResult Update(B3ChallengeDomain.Task entity, bool autoSave = true);
        OperationResult Delete(int id);
        B3ChallengeDomain.Task GetById(int id);
        List<B3ChallengeDomain.Task> GetAll();
    }
}
