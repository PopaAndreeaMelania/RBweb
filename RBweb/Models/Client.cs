using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RBweb.Models
{
    public class Client
    {
        public int ID { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public ICollection<Comanda>? Comenzi { get; set; }
    }
}
