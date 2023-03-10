using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Rabbit
{
    public enum OperationType
    {
        Insert = 0,
        Update = 1,
        Delete = 2,
        Select = 3,
    }

    public class OperationMessage
    {
        public string MessageId { get; set; }
        public OperationType OperationType { get; set; } // 0 = Insert, 1 = Update, 2 = Delete, 3 = Select
        public string Message { get; set; }
    }
}
