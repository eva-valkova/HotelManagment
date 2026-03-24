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
    internal class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Number { get; set; }


        public int Capacity { get; set; }


        public string Type { get; set; }


        public bool IsFree { get; set; }


        public decimal PriceAdult { get; set; }


        public decimal PriceChild { get; set; } 



    }
}
