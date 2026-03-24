using System.Runtime.InteropServices;
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
    internal class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //da go slagam v klasovete
        public int ClientID { get; set; }
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsOverEighteen { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }



    }
}
