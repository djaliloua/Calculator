using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorModel
{
    [Table("Operations")]
    public class Operation
    {
        public int Id { get; set; }
        public string OpValue { get; set; } 
        public string OpResult { get; set; }
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
