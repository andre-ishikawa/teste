using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoCotacaoMoeda.Models
{
    public class DadosCotacao
    {
        public Decimal vlr_cotacao { get; set; }

        public int cod_cotacao { get; set; }

        public DateTime dat_cotacao { get; set; }
    }
}
