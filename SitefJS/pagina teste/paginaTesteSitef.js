

var flagEntradaDados = false;
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

function envioDadosUsuario()
{
    $("#entradaDadosUsuario").hide();
    flagEntradaDados = false;
    var req = { retornofuncao: 0, comando:0, tipocampo:0, buffer: $("#txtDadosUsuario").val(), continua:0 };

    continuacao(req);
}


$("#btnContinuar").click(function(){
    $("#btnContinuar").hide();
    flagEntradaDados = false;
    var req = { retornofuncao: 0, comando:0, tipocampo:0, buffer: "", continua:0 };

    continuacao(req);
})

$("#btnInicio").click(function(){
    inicia();
});

var ReqInicializacao = { 
    testeAdd: '2',
    Funcao: '0', 
    NumCupom: '1234', 
    Valor: '9,99', 
    Data: '20170621', 
    Hora: '092000', 
    Operador: '', 
    Restricoes: ''
};

function inicia()
{
    $("#lblStatusPost").show();
    $.post("http://localhost:8080/", JSON.stringify(ReqInicializacao),
        function(data)
        {
            var obj = $.parseJSON(data);

            $("#lblStatusPost").hide();
            console.log(data);
            
            if(obj.retornofuncao == 10000)
            {
                console.log("retorno buffer: " + obj.buffer);
                escreveValoresPagina(obj);
                console.log("retorno função: " + obj.retornofuncao);
                continuacao(obj);    
            }            
        }
    )
    .fail(
        function()
        {
            $("#lblStatusPost").hide();
            $("#erro").toggle();
        
            setTimeout(
            function()
            {
                $("#erro").toggle();
            }, 10000)

        }
    );
   
}

function escreveValoresPagina(data)
{
    console.log("entrou function escreveValoresPagina()");
    
    console.log("escreveValoresPagina: var data.buffer = " + data.buffer);
    $("#lblValConteudoBuffer").text(data.buffer);
    $("#lblValRetFunc").text(data.retornofuncao);
    $("#lblValComando").text(data.comando);
    $("#lblValTipoCampo").text(data.tipocampo);
}



function continuacao(data)
{
    console.log("entrou function continuacao()");
    $("#lblStatusPost").show();
    $.post("http://localhost:8080/", JSON.stringify(data), function(dataRec){

        var obj = $.parseJSON(dataRec);

        $("#lblStatusPost").hide();
        escreveValoresPagina(obj);
        if(obj.retornofuncao == 10000)
        {
            if(obj.comando == 0)
            {
                rotinaResultado(obj.tipocampo, obj.buffer);
            }
            else
            {
                rotinaColeta(obj);
            }

            if(!flagEntradaDados)
            {
                continuacao(obj);
            }
        }
        
    }).fail(
        function()
        {
            $("#lblStatusPost").hide();
            $("#erro").toogle();
        
            setTimeout(
            function()
            {
                $("#erro").toogle();
            }, 10000)

        }
    );
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
            
            switch(data.tipocampo)
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
