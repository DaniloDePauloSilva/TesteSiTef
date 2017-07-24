using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SitefJS.ModelJson;
using SitefJS.Sitef;

namespace SitefJS.Srv
{
    public class SrvJs
    {
        public static bool listenerAtivo;

        public static void iniciarListener(string[] strUrl)
        {
            HttpListener listener = new HttpListener();
            byte[] buffer;
            Stream output;

            for (int i = 0; i < strUrl.Length; i++)
            {
                listener.Prefixes.Add(strUrl[i]);
            }

            listener.Start();
            listenerAtivo = true;

            while (listenerAtivo)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                HttpListenerResponse response = context.Response;
                response.AppendHeader("Access-Control-Allow-Origin", "*");
                

                string conteudoPostReq = "";

                if (request.HttpMethod.Equals("post", StringComparison.OrdinalIgnoreCase))
                {
                    conteudoPostReq = new StreamReader(request.InputStream, Encoding.UTF8).ReadToEnd().Replace("\"", "\'").Replace(",", ", ");

                    RespCmd resp = verificaRequisicao(conteudoPostReq);

                    string JsonResp = retornaJsonSerializado(resp);

                    buffer = Encoding.UTF8.GetBytes(JsonResp);
                    
                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes("ERRO");
                }

                response.ContentLength64 = buffer.Length;
                output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                output.Close();

            }

            listener.Stop();
        }

        public static string retornaJsonSerializado(RespCmd resp)
        {
            if (resp is RespCmdContinua)
            {
                RespCmdContinua respCont = (RespCmdContinua)resp;
                return JsonConvert.SerializeObject(respCont);
            }
            else
            {
                return JsonConvert.SerializeObject(resp);
            }
        }
        
        public static RespCmd verificaRequisicao(string conteudoPostReq)
        {
            ReqCmd req = new ReqCmd(conteudoPostReq);
            RespCmd resp = null;

            switch (req.funcaoSiTef)
            {
                case ReqCmd.eIdCmd.iniciaFuncaoSiTefInterativo:

                    ReqCmdInicia reqInicia = JsonConvert.DeserializeObject<ReqCmdInicia>(conteudoPostReq);
                    
                    resp = new RespCmd();

                    clsTef.ConfiguraIntSiTefInterativo("127.0.0.1", "00000000", "SK000001");
                    resp.retornoFuncao = clsTef.IniciaFuncaoSiTefInterativo(reqInicia.funcao, reqInicia.valor, reqInicia.numeroCupom, reqInicia.dataFiscal, reqInicia.horaFiscal, reqInicia.operador, reqInicia.restricoes);

                    break;

                case ReqCmd.eIdCmd.continuaFuncaoSiTefInterativo:
                    ReqCmdContinua reqContinua = JsonConvert.DeserializeObject<ReqCmdContinua>(conteudoPostReq);
                    resp = new RespCmdContinua();

                    int comando = 0;
                    int tipoCampo = 0;
                    short tamanhoMaximo = 0;
                    short tamanhoMinimo = 0;
                    byte[] bufferSitef = new byte[20000];
                    byte[] bufferReq = Encoding.UTF8.GetBytes(reqContinua.buffer);

                    for (int i = 0; i < bufferReq.Length; i++)
                    {
                        bufferSitef[i] = bufferReq[i];
                    }

                    ((RespCmdContinua)resp).retornoFuncao = clsTef.Continua(ref comando, ref tipoCampo, ref tamanhoMinimo, ref tamanhoMaximo, bufferSitef, bufferSitef.Length, reqContinua.continua);

                    ((RespCmdContinua)resp).comando = comando;
                    ((RespCmdContinua)resp).tipoCampo = tipoCampo;
                    ((RespCmdContinua)resp).tamanhoMinimo = tamanhoMinimo;
                    ((RespCmdContinua)resp).tamanhoMaximo = tamanhoMaximo;

                    string contBuf = Encoding.UTF8.GetString(bufferSitef);

                    ((RespCmdContinua)resp).buffer = contBuf.Substring(0, contBuf.IndexOf("\0"));


                    break;

                case ReqCmd.eIdCmd.finalizaFuncaoSiTefInterativo:
                    ReqCmdFinaliza reqFin = JsonConvert.DeserializeObject<ReqCmdFinaliza>(conteudoPostReq);

                    resp = new RespCmd();

                    resp.retornoFuncao = clsTef.Finaliza(reqFin.confirma, reqFin.numeroCupom, reqFin.dataFiscal, reqFin.horaFiscal);

                    break;

                default:
                    resp = new RespCmd();
                    resp.retornoFuncao = 9999;

                    break;
            }
            
            return resp;
        }
    }
}
