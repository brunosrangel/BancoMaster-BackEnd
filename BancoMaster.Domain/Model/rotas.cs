using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoMaster.Domain.Model
{
    public class rotas
    {
        public int IdRota { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public int Valor { get; set; }

        public rotas() { }
    }
}
