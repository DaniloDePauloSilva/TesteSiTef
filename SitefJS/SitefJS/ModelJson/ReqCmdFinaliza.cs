using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefJS.ModelJson
{
    public class ReqCmdFinaliza : ReqCmd
    {
        public short confirma { get; set; }
        public string numeroCupom { get; set; }
        public string dataFiscal { get; set; }
        public string horaFiscal { get; set; }
        public string paramAdic { get; set; }

    }
}
