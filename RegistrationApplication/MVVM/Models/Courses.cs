namespace RegistrationApplication.MVVM.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseExpectation { get; set; }
        public int CourseProformaId { get; set; }
        public virtual CourseProforma CourseProforma { get; set; }
    }
    public class CourseProforma
    {
        public int CourseProformaId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
