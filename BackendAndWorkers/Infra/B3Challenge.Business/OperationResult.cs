using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Business
{
    public class OperationResult
    {
        public bool Success { get;  set; }
        public List<string> Errors { get;  set; }
    }
}
