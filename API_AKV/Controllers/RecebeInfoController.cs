using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace AKV_API.Controllers
{
     [RoutePrefix("api/RecebeInfo")]
    public class RecebeInfoController : ApiController
    {

        #region Receber dados URA
        
        [HttpGet]
        [ActionName("RecebeCartao")]
        [Route("Receber/DadosCartao")]
        public HttpResponseMessage DadosURA(int cliCodigo, string numCart, string validadeCart, string CVV, string retorno)
        {
            Models.CartaoCli objCartao = new Models.CartaoCli(cliCodigo, numCart, validadeCart, CVV, retorno);
            DAL.CartaoCliente dados = new DAL.CartaoCliente();

            int codRet = Convert.ToInt32(retorno);

            dados.GravaLog(objCartao,Request.RequestUri.ToString());

            try
            {
                if (codRet < 1 || codRet > 5)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Código de Retorno Invalido");
                }
                else
                {
                    if (codRet == 1)
                    {
                        if (numCart == string.Empty || validadeCart == string.Empty || CVV == string.Empty || numCart == null || CVV == null || validadeCart == null )
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, "Não foi possivel coletar todas as informações do cartão. Tente novamente!");
                        }
                        else
                        {
                            dados.GravaCartao(objCartao);
                            return Request.CreateResponse(HttpStatusCode.OK, "Dados do cartão coletados com Sucesso!");
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
            }
            catch (Exception ex)
            {
               string text = ex.Message;
               return Request.CreateResponse(HttpStatusCode.InternalServerError, "Não foi possivel gravar as informações do cartão, tentar novamente");
            }
            
        }
        #endregion

    }
}