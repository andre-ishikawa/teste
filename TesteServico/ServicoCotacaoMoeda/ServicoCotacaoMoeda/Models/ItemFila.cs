using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoCotacaoMoeda.Models
{
    public class ItemFila
    {
        public string Moeda { get; set; }
        
        public DateTime Data_Inicio { get; set; }

        public DateTime Data_Fim { get; set; }
    }
}
