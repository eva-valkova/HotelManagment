using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagment.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, StringLength(10)]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public bool IsAdult { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}