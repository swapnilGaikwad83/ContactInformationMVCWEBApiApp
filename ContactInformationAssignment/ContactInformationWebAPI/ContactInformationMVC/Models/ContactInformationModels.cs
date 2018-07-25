using System.ComponentModel.DataAnnotations;

namespace ContactInformationMVC.Models
{
    public class ContactInformationModels
    {
        public int ContactID { get; set; }

        [Required(ErrorMessage = "Please provide FirsName") ]
        public string FirsName { get; set; }

        [Required(ErrorMessage = "Please provide LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please provide Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide PhoneNumber")]
        public string PhoneNumber { get; set; }
                
        public bool ContactStatus { get; set; }
    }
}