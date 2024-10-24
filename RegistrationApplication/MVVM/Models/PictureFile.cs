namespace RegistrationApplication.MVVM.Models
{
    public class PictureFile
    {
        public int PictureFileId { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public byte[] Picture { get; set; }
        public int TrainerId { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
    public class Trainer
    {
        public int TrainerId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string ShortDescription { get; set; }

        public string EducationLevel { get; set; }
        public string Gender { get; set; }

        public string JobTitle { get; set; }
        public DateTime Birthday { get; set; }

        public virtual PictureFile PictureFile { get; set; }
        public virtual IList<Experience> Experiences { get; set; }
    }
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int TrainerId { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
