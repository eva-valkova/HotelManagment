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
    internal class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //da go slagam v klasovete
       
            public int Id { get; set; }

            public int RoomId { get; set; }
            public Room ReservedRoom { get; set; }

            public int UserId { get; set; }
            public User CreatedByUser { get; set; }

            public List<Client> Guests { get; set; } = new List<Client>();


            public DateTime CheckInDate { get; set; }


            public DateTime CheckOutDate { get; set; }


            public bool HasBreakfast { get; set; }

            public bool IsAllInclusive { get; set; }


            public decimal TotalAmount { get; set; }
    }
}