using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SitefJS.ModelJson
{
    public class ReqCmd
    {
        public ReqCmd() { }

        public ReqCmd(string json)
        {
            deserializarString(json);
        }

        public int idCmd { get; set;}

        public eIdCmd funcaoSiTef;
       
        public enum eIdCmd{
            iniciaFuncaoSiTefInterativo = 1,
            continuaFuncaoSiTefInterativo = 2,
            finalizaFuncaoSiTefInterativo = 3
        };

        public void deserializarString(string conteudo)
        {
            ReqCmd req = JsonConvert.DeserializeObject<ReqCmd>(conteudo);

            this.idCmd = req.idCmd;

            if (this.idCmd != 0)
            {
                funcaoSiTef = (eIdCmd)this.idCmd;
            }

            req = null;

        }
    }
}
