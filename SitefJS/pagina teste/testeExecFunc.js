var flagEntradaDados = false;

var valor;
var numCupom;
var dataFiscal;
var horaFiscal;


$("#lblStatusPost").hide();
$("#erro").hide();
$("#entradaDadosUsuario").hide();
$("#btnContinuar").hide();

$("#btnDadosUsuario").click(function(){
    envioDadosUsuario();
});

$("#txtDadosUsuario").keypress(function(e){
    if(e.which == 13)
    {
        envioDadosUsuario();
    }
});


$("#btnContinuar").click(function(){
    $("#btnContinuar").hide();
    flagEntradaDados = false;    
    fluxoTransacao(10000, numCupom, dataFiscal, horaFiscal, "");
});

$("#btnInicio").click(function(){
    inicia();
});

function inicia()
{
    valor = "88,88";
    numCupom = "8888";
    dataFiscal = "20170626";
    horaFiscal = "101000";

    var retornoFunc = iniciaFuncaoSiTefInterativo(0, valor, numCupom, dataFiscal, horaFiscal, "", "");

    if(retornoFunc == 10000)
    {
       fluxoTransacao(retornoFunc, numCupom, dataFiscal, horaFiscal, "");
    }
}

function envioDadosUsuario()
{
    $("#entradaDadosUsuario").hide();
    flagEntradaDados = false;
    fluxoTransacao(10000, numCupom, dataFiscal, horaFiscal, $("#txtDadosUsuario").val());
}

function escreveValoresPagina(objRetorno)
{
    console.log("entrou function escreveValoresPagina()");
    
    console.log("escreveValoresPagina: var data.buffer = " + objRetorno.buffer);
    $("#lblValConteudoBuffer").text(objRetorno.buffer);
    $("#lblValRetFunc").text(objRetorno.retornoFuncao);
    $("#lblValComando").text(objRetorno.comando);
    $("#lblValTipoCampo").text(objRetorno.tipoCampo);
}


function fluxoTransacao(retornoFunc, cupomFisc, dataFiscal, horario, dadosUsuario)
{

    console.log("==========entrou fluxoTransacao=========");

    var objRetorno = null;

    while(retornoFunc == 10000)
    {
        console.log("==========retornoFunc == 10000==========");
        objRetorno = continuaFuncaoSiTefInterativo(dadosUsuario, 0);

        console.log("fluxoTransacao: " + objRetorno);

        if(objRetorno != null)
        {
            escreveValoresPagina(objRetorno);

            console.log("objRetorno = " + objRetorno);

            retornoFunc = objRetorno.retornoFuncao;

            if(objRetorno.comando == 0)
            {
                rotinaResultado(objRetorno.tipoCampo, objRetorno.buffer);
            }
            else
            {
                rotinaColeta(objRetorno);
            }

            if(flagEntradaDados)
            {
                return;
            }

        }
    }

    if(objRetorno.retornoFuncao == 0)
    {
        objRetorno = finalizaTransacaoSiTefInterativo(1, cupomFisc, dataFiscal, horaFiscal, "");
    }
    else
    {
        objRetorno = finalizaTransacaoSiTefInterativo(0, cupomFisc, dataFiscal, horaFiscal, "");
    }
}


function rotinaColeta(data)
{
    var mensagem = data.buffer;
    
    switch(data.comando)
    {
        case 1:
            $("#lblStatus").text("Mensagem visor operador: " + mensagem);
            break;
        case 2:
            $("#lblStatus").text("Mensagem visor cliente: " + mensagem);
            break;
        case 3:
            $("#lblStatus").text("Mensagem para os dois visores: " + mensagem);
            break;
        case 4:
            $("#lblStatus").text("Mensagem Visor: " + mensagem)
            break;
        
        case 20:
            $("#lblStatus").text("Coleta Sim(0) / Não(1) : " + mensagem );
            entradaDadosUsuario();
            return;

        case 21:
            $("#lblStatus").text(mensagem);
            entradaDadosUsuario();
            return;

        case 22:
            $("#lblStatus").text(mensagem);
            aguardaBtnContinuar();

        case 30:
            
            switch(data.tipoCampo)
            {
                case 140:
                    $("#lblStatus").text("DIGITE A DATA DA PRIMEIRA PARCELA NO FORMATO DDMMAAAA");
                    entradaDadosUsuario();                    
                    return;

                case 505:
                    $("#lblStatus").text("DIGITE O NÚMERO DE PARCELAS");
                    entradaDadosUsuario();
                    return;

                case 506:
                    $("#lblStatus").text("DIGITE A DATA DO PRÉ-DATADO NO FORMATO ddMMaaaa:");
                    entradaDadosUsuario();
                    return;

                case 511:
                    $("#lblStatus").text("DIGITE O NÚMERO DE PARCELAS CDC");
                    entradaDadosUsuario();
                    return;

                case 512:
                    $("#lblStatus").text("DIGITE O NÚMERO DO CARTÃO");
                    entradaDadosUsuario();
                    return;

                case 513:
                    $("#lblStatus").text("DIGITE A DATA DE VENCIMENTO DO CARTÃO");
                    entradaDadosUsuario();
                    return;
                
                case 514:
                console.log("===comando 30, tipoCampo 514===");
                    $("#lblStatus").text("DIGITE O CÓDIGO DE SEGURANÇA DO CARTÃO");
                    entradaDadosUsuario();
                    return;
                
                case 515:
                    $("#lblStatus").text("DIGITE A DATA DA TRANSAÇÃO A SER CANCELADA OU REIMPRESSA (DDMMAAAA)");
                    entradaDadosUsuario();
                    return;

                case 516:
                    $("#lblStatus").text("DIGITE O NÚMERO DO DOCUMENTO A SER CANCELADO OU REIMPRESSO: ");
                    entradaDadosUsuario();
                    return;
            }

        case 34:
            switch(data.tipocampo)
            {
                case 130:
                    $("#lblStatus").text(data.buffer);
                    entradaDadosUsuario();
                    return;

                case 146:
                    $("#lblStatus").text(data.buffer);
                    entradaDadosUsuario();
                    return;

                case 504:
                    $("#lblStatus").text(data.buffer);
                    entradaDadosUsuario();
                    return;

                case 714:
                    $("#lblStatus").text(data.buffer);
                    entradaDadosUsuario();
                    return;
            }
    }
}

function entradaDadosUsuario()
{
    flagEntradaDados = true;
    $("#txtDadosUsuario").val("");
    $("#entradaDadosUsuario").show();
}

function aguardaBtnContinuar()
{
    flagEntradaDados = true;
    $("#btnContinuar").show();   
}

function rotinaResultado(tipoCampo, buffer)
{
    switch(tipoCampo)
    {
        case 121:
        $("#lblStatus").text("COMPROVANTE CLIENTE: \n\n" + buffer);
        aguardaBtnContinuar();
        break;

        case 122:
        $("#lblStatus").text("COMPROVANTE ESTABELECIMENTO: \n\n" + buffer);
        aguardaBtnContinuar();
        break;

        default:
        $("#lblStatus").text("Tipo Campo - " + tipoCampo + " | buffer: " + buffer);
        break;
    }

    

}
