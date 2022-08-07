using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace customer.form.api.Models
{
    public class Customer
    {
        [Required]
        [DataMember(Name = "firstName")]
        public string? FirstName { get; set; }

        [Required]
        [DataMember(Name = "lastName")]
        public string? LastName { get; set; }
    }
}
