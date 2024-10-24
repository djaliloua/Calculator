using RegistrationApplication.MVVM.Models;

namespace RegistrationApplication.DataAccessLayer
{
    public static class StaticDataSource
    {
        public static List<string> Genders = new List<string>() { "M", "F"};
        public static List<string> EducationLevel = new List<string>() { "Phd", "Master", "Bachelor", "High School" };
        public static List<Course> Courses = new List<Course>()
        {
            new(){CourseId=1, 
                CourseProformaId=1, 
                CourseName="Machine learning", 
                CourseExpectation="Niente", CourseDescription="aaaaa", 
                CourseProforma=new(){CourseProformaId=1, CourseId=1}},
            new(){CourseId=2,
                CourseProformaId=2,
                CourseName="Deep learning",
                CourseExpectation="Niente", CourseDescription="aaaaa",
                CourseProforma=new(){CourseProformaId=2, CourseId=2}},
            new(){CourseId=3,
                CourseProformaId=3,
                CourseName="Python programming",
                CourseExpectation="Niente", CourseDescription="aaaaa",
                CourseProforma=new(){CourseProformaId=3, CourseId=3}},
            new(){CourseId=4,
                CourseProformaId=4,
                CourseName="C# programming",
                CourseExpectation="Niente", CourseDescription="aaaaa",
                CourseProforma=new(){CourseProformaId=4, CourseId=4}},
            new(){CourseId=5,
                CourseProformaId=5,
                CourseName="Artificial Intelligence",
                CourseExpectation="Niente", CourseDescription="aaaaa",
                CourseProforma=new(){CourseProformaId=5, CourseId=5}},
        };
    }
}
