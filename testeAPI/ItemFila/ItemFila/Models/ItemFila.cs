using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItemFila.Models
{
    public class ItemFila
    {
        [Required]
        public string Moeda { get; set; }

        [Required]
        public DateTime Data_Inicio { get; set; }

        public DateTime Data_Fim { get; set; }
    }

}