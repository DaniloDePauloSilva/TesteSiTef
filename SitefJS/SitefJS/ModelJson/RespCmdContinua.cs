using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefJS.ModelJson
{
    public class RespCmdContinua : RespCmd
    {
        public int comando { get; set; }
        public int tipoCampo { get; set; }
        public string buffer { get; set; }
        public int tamanhoMinimo { get; set; }
        public int tamanhoMaximo { get; set; }
        
    }
}
