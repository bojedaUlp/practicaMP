using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace pruebaPracticas.Models
{
    public class Auto
    {
        [Key]
        public int Id_Auto { get; set; }
        public String Patente { get; set; }
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public int Año { get; set; }
        public int Kms { get; set; }
        public String Img1 { get; set; }
        public String Img2 { get; set; }

    }
}
