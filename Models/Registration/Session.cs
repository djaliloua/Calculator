using System.ComponentModel.DataAnnotations;

namespace Models.Registration
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionDescription { get; set; }
        public string SessionType { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Platform { get; set; }
        public int TrainerId { get; set; }
        public int CourseId { get; set; }
    }
}
