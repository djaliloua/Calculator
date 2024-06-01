using System.ComponentModel.DataAnnotations.Schema;

namespace Calculator.MVVM.Models
{
    [Table("OperationsTable")]
    public class Operation
    {
        public int Id { get; set; }
        public string OpValue { get; set; }  // operation Value
        public string OpResult { get; set; } // Operation Result
        public DateTime OperationDate { get; set; }
        public Operation(string value, string result)
        {
            OpValue = value;
            OpResult = result;
            OperationDate = DateTime.Now;
        }
        public Operation() { }
    }
}
