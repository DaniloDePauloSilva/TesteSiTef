using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SitefJS.Sitef
{
    public class clsTef
    {
        public static int ConfiguraIntSiTefInterativo(string enderecoIp, string codEstab, string numTerm)
        {
            try
            {
                byte[] bIp = Encoding.ASCII.GetBytes(enderecoIp + "\0");
                byte[] bEstab = Encoding.ASCII.GetBytes(codEstab + "\0");
                byte[] bNumTerm = Encoding.ASCII.GetBytes(numTerm + "\0");

                return ConfiguraIntSiTefInterativo(bIp, bEstab, bNumTerm, 0);
            }
            catch
            {
                throw;
            }
        }

        public static int IniciaFuncaoSiTefInterativo(int funcao, string valor, string numCupom, string dataFiscal, string horario, string operador, string restricoes)
        {
            try
            {
                byte[] bValor = Encoding.ASCII.GetBytes(valor + "\0");
                byte[] bNumCupom = Encoding.ASCII.GetBytes(numCupom + "\0");
                byte[] bDataFiscal = Encoding.ASCII.GetBytes(dataFiscal + "\0");
                byte[] bHorario = Encoding.ASCII.GetBytes(horario + "\0");
                byte[] bOperador = Encoding.ASCII.GetBytes(operador + "\0");
                byte[] bRestr = Encoding.ASCII.GetBytes(restricoes + "\0");

                return IniciaFuncaoSiTefInterativo(funcao, bValor, bNumCupom, bDataFiscal, bHorario, bOperador, bRestr);
            }
            catch
            {
                throw;
            }
        }

        public static int IniciaFuncaoAASiTefInterativo(int funcao, string valor, string cupomFiscal, string dataFiscal, string horario, string operador, string paramAdic, string produtos)
        {
            try
            {
                byte[] bValor = Encoding.ASCII.GetBytes(valor + "\0");
                byte[] bCupom = Encoding.ASCII.GetBytes(cupomFiscal + "\0");
                byte[] bDataFiscal = Encoding.ASCII.GetBytes(dataFiscal + "\0");
                byte[] bHorario = Encoding.ASCII.GetBytes(horario + "\0");
                byte[] bOperador = Encoding.ASCII.GetBytes(operador + "\0");
                byte[] bParamAdic = Encoding.ASCII.GetBytes(paramAdic + "\0");
                byte[] bProdutos = Encoding.ASCII.GetBytes(produtos + "\0");

                return IniciaFuncaoAASiTefInterativo(funcao, bValor, bCupom, bDataFiscal, bHorario, bOperador, bParamAdic, bProdutos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Continua(ref int comando, ref int tipoCampo, ref short tamMin, ref short tamMax, byte[] buffer, int tamBuffer, int continua)
        {
            try
            {
                return ContinuaFuncaoSiTefInterativo(ref comando, ref tipoCampo, ref tamMin, ref tamMax, buffer, tamBuffer, continua);
            }
            catch
            {
                throw;
            }
        }

        public static int Finaliza(short confirma, string cupom, string dataFiscal, string horaFiscal)
        {
            try
            {
                byte[] bCupom = Encoding.ASCII.GetBytes(cupom + "\0");
                byte[] bDataFiscal = Encoding.ASCII.GetBytes(dataFiscal + "\0");
                byte[] bHoraFiscal = Encoding.ASCII.GetBytes(horaFiscal + "\0");

                return FinalizaTransacaoSiTefInterativo(confirma, bCupom, bDataFiscal, bHoraFiscal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int ObtemQuantidadeTransacoesPendentes(string dataFiscal, string cupomFiscal)
        {
            try
            {
                byte[] bDataFiscal = Encoding.ASCII.GetBytes(dataFiscal + "\0");
                byte[] bCupomFiscal = Encoding.ASCII.GetBytes(cupomFiscal + "\0");

                return ObtemQuantidadeTransacoesPendentes(bDataFiscal, bCupomFiscal);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ObtemChaveSeguranca(string trilha2, string chaveAbertura, ref string chaveSeguranca)
        {
            try
            {
                byte[] bTrilha2 = Encoding.ASCII.GetBytes(trilha2 + "\0");
                byte[] bChaveAbertura = Encoding.ASCII.GetBytes(chaveAbertura + "\0");
                byte[] bChaveSeguranca = new byte[64];

                int retorno = ObtemChaveSeguranca(bTrilha2, bChaveAbertura, bChaveSeguranca);

                chaveSeguranca = Encoding.ASCII.GetString(bChaveSeguranca);

                return retorno;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int LeSenhaDireto(string chaveSeguranca, ref string senhaCliente)
        {
            try
            {
                byte[] bChaveSeguranca = Encoding.ASCII.GetBytes(chaveSeguranca + "\0");
                byte[] bSenhaCliente = new byte[20];

                int retorno = LeSenhaDireto(bChaveSeguranca, bSenhaCliente);

                senhaCliente = Encoding.ASCII.GetString(bSenhaCliente);

                return retorno;
            }
            catch
            {
                throw;
            }

        }

        public static int ObtemSenha(string senhaCapturada, string chaveSeguranca, string chaveAbertura, int tamMaxSenhaAberta, ref string senhaAberta)
        {
            try
            {
                byte[] bSenhaCapturada = Encoding.ASCII.GetBytes(senhaCapturada + "\0");
                byte[] bChaveSeguranca = Encoding.ASCII.GetBytes(chaveSeguranca + "\0");
                byte[] bChaveAbertura = Encoding.ASCII.GetBytes(chaveAbertura + "\0");
                byte[] bSenhaAberta = new byte[20];
                //byte[] bTamMaxSenhaAberta = Encoding.ASCII.GetBytes("");

                int retorno = ObtemSenha(bChaveAbertura, bChaveSeguranca, bSenhaCapturada, bSenhaAberta, bSenhaAberta.Length);

                senhaAberta = Encoding.ASCII.GetString(bSenhaAberta);

                return retorno;
            }
            catch
            {
                throw;
            }
        }

        public static int ConfiguraIntSiTefInterativoEx(string ip, string loja, string term, string paramAdic)
        {
            try
            {
                byte[] bIp = Encoding.ASCII.GetBytes(ip + "\0");
                byte[] bLoja = Encoding.ASCII.GetBytes(loja + "\0");
                byte[] bTerm = Encoding.ASCII.GetBytes(term + "\0");
                byte[] bParamAdic = Encoding.ASCII.GetBytes(paramAdic + "\0");

                return ConfiguraIntSiTefInterativoEx(bIp, bLoja, bTerm, 0, bParamAdic);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int LeCartaoDireto(string msgDisplay, ref string trilha1, ref string trilha2)
        {
            try
            {
                byte[] bMsgDisplay = Encoding.ASCII.GetBytes(msgDisplay + "\0");
                byte[] bTrilha1 = new byte[100];
                byte[] bTrilha2 = new byte[100];

                int retorno = LeCartaoDireto(bMsgDisplay, bTrilha1, bTrilha2);

                trilha1 = Encoding.UTF8.GetString(bTrilha1);
                trilha2 = Encoding.UTF8.GetString(bTrilha2);

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [DllImport("CliSiTef32I.dll", EntryPoint = "LeCartaoDireto", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int LeCartaoDireto(byte[] pMsgDisplay, byte[] trilha1, byte[] trilha2);

        [DllImport("CliSitef32I.dll", EntryPoint = "VerificaPresencaPinPad", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int VerificaPresencaPinPad();

        [DllImport("CliSitef32I.dll", EntryPoint = "ObtemSenha", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ObtemSenha(byte[] chaveAbertura, byte[] chaveSeguranca, byte[] senhaCapturada, byte[] senhaAberta, int tamMaxSenhaAberta);

        [DllImport("CliSitef32I.dll", EntryPoint = "LeSenhaDireto", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int LeSenhaDireto(byte[] chaveSeguranca, byte[] senhaCliente);

        [DllImport("CliSitef32I.dll", EntryPoint = "LeSenhaInterativo", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int LeSenhaInterativo(byte[] chaveSeguranca);

        [DllImport("CliSitef32I.dll", EntryPoint = "ObtemChaveSeguranca", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ObtemChaveSeguranca(byte[] trilha2, byte[] chaveAbertura, byte[] chaveSeguranca);

        [DllImport("CliSitef32I.dll", EntryPoint = "ObtemQuantidadeTransacoesPendentes", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ObtemQuantidadeTransacoesPendentes(byte[] dataFiscal, byte[] cupomFiscal);

        [DllImport("CliSitef32I.dll", EntryPoint = "ConfiguraIntSiTefInterativo", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConfiguraIntSiTefInterativo(byte[] enderecoIP, byte[] codEstab, byte[] numTerm, short confRes);

        [DllImport("CliSitef32I.dll", EntryPoint = "ConfiguraIntSiTefInterativoEx", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConfiguraIntSiTefInterativoEx(byte[] enderecoIP, byte[] codEstab, byte[] numTerm, short confRes, byte[] paramAdic);

        [DllImport("CliSitef32I.dll", EntryPoint = "IniciaFuncaoSiTefInterativo", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int IniciaFuncaoSiTefInterativo(int funcao, byte[] valor, byte[] numCupom, byte[] dataFiscal, byte[] horario, byte[] operador, byte[] restricoes);

        [DllImport("CliSitef32I.dll", EntryPoint = "IniciaFuncaoAASiTefInterativo", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int IniciaFuncaoAASiTefInterativo(int Funcao, byte[] pValor, byte[] pCupomFiscal, byte[] pDataFiscal, byte[] pHorario, byte[] pOperador, byte[] pAdicionais, byte[] pProdutos);

        [DllImport("CliSitef32I.dll", EntryPoint = "ContinuaFuncaoSiTefInterativo", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int ContinuaFuncaoSiTefInterativo(ref int pComando, ref int pTipoCampo, ref short pTamMinimo, ref short pTamMaximo, byte[] pBuffer, int TamBuffer, int Continua);

        [DllImport("CliSitef32I.dll", EntryPoint = "FinalizaTransacaoSiTefInterativo", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int FinalizaTransacaoSiTefInterativo(short pConfirma, byte[] pCupomFiscal, byte[] pDataFiscal, byte[] pHoraFiscal);
    }
}
