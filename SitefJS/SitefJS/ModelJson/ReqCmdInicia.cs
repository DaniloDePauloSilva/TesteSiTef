using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefJS.ModelJson
{
    public class ReqCmdInicia : ReqCmd
    {
        public int funcao { get; set; }
        public string valor { get; set; }
        public string numeroCupom { get; set; }
        public string dataFiscal { get; set; }
        public string horaFiscal { get; set; }
        public string operador { get; set; }
        public string restricoes { get; set; }

    }
}
