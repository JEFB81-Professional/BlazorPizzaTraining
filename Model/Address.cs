
using System.ComponentModel.DataAnnotations;
namespace BlazingPizza
{
    public class Address
    {
        public int Id { get; set; }

        [Required, MinLength(3, ErrorMessage = "Please use a Name bigger than 3 letters."), MaxLength(100, ErrorMessage = "Please use a Name less than 100 letters.")]
        public string Name { get; set; }

        [Required, MinLength(5, ErrorMessage = "Please use an Address bigger than 5 letters."), MaxLength(100, ErrorMessage = "Please use an Address less than 100 letters.")]
        [Display(Name = "Main Address")]
        public string Line1 { get; set; }

        public string Line2 { get; set; }
        [Required(ErrorMessage = "You must set a city")]
        public string City { get; set; }

        public string Region { get; set; }

        [Required(ErrorMessage = "You must set a postal code")]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
    }
}
