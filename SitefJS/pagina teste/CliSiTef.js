
var strUrl = "http://127.0.0.1:8080/";

function iniciaFuncaoSiTefInterativo(func, val, numCupom, dataFis, horaFis, op, restr)
{
    var reqInicia = {
        idCmd: '1', 
        funcao: func, 
        valor: val, 
        numeroCupom: numCupom, 
        dataFiscal: dataFis, 
        horaFiscal: horaFis, 
        operador: op,
        restr: restr
    };

    var retornoPost = -9999;
 
  jQuery.ajax({
   type: "POST",
   url: strUrl,
   cors: true,
   async : false,
   data : JSON.stringify(reqInicia),
   success :  function(data)
        {
            console.log("iniciaFuncaoSiTefInterativo - String Retorno Post: " + data);
            var obj = $.parseJSON(data);            
            console.log("iniciaFuncaoSiTefInterativo - Objeto Retorno Post: " + obj);
            
            retornoPost = obj.retornoFuncao;  
            console.log("SUCESSO NA REQUISIÇÃO - Código retorno = " + retornoPost);          
        },
    fail: function(){
        console.log("FALHA NA REQUISIÇÃO POST");
        retornoPost = -9998;
    }
  });

  return retornoPost;

}

function continuaFuncaoSiTefInterativo(buffer, continua)
{
    var reqCmdContinua ={
        idCmd: '2',
        buffer: buffer,
        continua: continua
    }; 

    var objRet = null;

    jQuery.ajax({
   type: "POST",
   url: strUrl,
   cors: true,
   async : false,
   data : JSON.stringify(reqCmdContinua),
   success :  function(data)
        {
            console.log("continuaFuncaoSiTefInterativo - String Retorno Post: " + data);
            objRet = $.parseJSON(data);            
            console.log("continuaFuncaoSiTefInterativo - Objeto Retorno Post: " + objRet);
            
        },
    fail: function(){
        console.log("FALHA NA REQUISIÇÃO POST");
       
    }
  });

  return objRet;

}

function finalizaTransacaoSiTefInterativo(conf, numCupom, dataFis, horaFis, paramAdic)
{
    var reqCmdFinaliza = {
        idCmd: '3',
        confirma: conf,
        numeroCupom: numCupom,
        dataFiscal: dataFis,
        horaFiscal: horaFis,
        paramAdic: paramAdic
    }

    jQuery.ajax({
   type: "POST",
   url: strUrl,
   cors: true,
   async : false,
   data : JSON.stringify(reqCmdFinaliza),
   success :  function(data)
        {
            console.log("finalizaTransacaoSiTefInterativo - String Retorno Post: " + data);
            var obj = $.parseJSON(data);            
            console.log("finalizaTransacaoSiTefInterativo - Objeto Retorno Post: " + obj);
            
            retornoPost = obj.retornoFuncao;  
        },
    fail: function(){
        console.log("FALHA NA REQUISIÇÃO POST");
        retornoPost = -9998;
    }
  });

  return retornoPost;

}




