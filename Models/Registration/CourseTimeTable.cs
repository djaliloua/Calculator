using System.ComponentModel.DataAnnotations;

namespace Models.Registration
{
    public class CourseTimeTable
    {
        [Key]
        public int CourseTimeTableId { get; set; }
        public DateOnly SessionDate { get; set; }
        public int SequenceNumber { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int SessionId { get; set; }
    }
}
