using System.ComponentModel.DataAnnotations;

namespace RegistrationApplication.MVVM.Models
{
    public class Participiant
    {
        [Key]
        public int ParticipiantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SessionId { get; set; }  
        public bool PreviousExperience { get; set; }
    }
}
