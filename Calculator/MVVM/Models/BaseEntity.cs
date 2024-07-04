using System.ComponentModel.DataAnnotations;

namespace Calculator.MVVM.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
