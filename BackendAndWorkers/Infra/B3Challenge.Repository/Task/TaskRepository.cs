using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Repository.Task
{
    public class TaskRepository : GenericRepository<Domain.Entities.Task>
    {
        public TaskRepository(MainDbContext context) : base(context)
        {

        
        }

        public void Update(Domain.Entities.Task entity)
        {
            this.DetachLocal(_ => _.Id == entity.Id);
            base.Update(entity);
        }
    }
}
