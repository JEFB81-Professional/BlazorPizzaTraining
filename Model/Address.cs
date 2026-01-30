
using System.ComponentModel.DataAnnotations;
namespace BlazingPizza
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must set a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must set a main address")]
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
