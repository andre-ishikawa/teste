using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoCotacaoMoeda.Models
{
    public class ResultadoFinal
    {
        public string ID_MOEDA { get; set; }

        public DateTime DATA_REF { get; set; }

        public Decimal VLR_COTACAO { get; set; }
    }
}
