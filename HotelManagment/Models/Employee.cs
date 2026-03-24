using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagment.Models
{
    internal class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public DateTime AppointmentDate { get; set; }

        public bool IsActive { get; set; }
        public DateTime? ReleaseDate { get; set; }


        public ICollection<Client>? Clients { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<Room>? Rooms { get; set; }


    }
}