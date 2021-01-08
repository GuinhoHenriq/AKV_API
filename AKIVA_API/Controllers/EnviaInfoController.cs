using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;

namespace AKIVA_API.Controllers
{
    public class EnviaInfoController : ApiController
    {

        #region Metodo que carrega informações do Banco
        [HttpGet]
        public HttpResponseMessage GetDadosBD(int cliCodigo)
        {
            Models.CartaoCli objCartao = new Models.DadosCart(cliCodigo);
            DAL.CartaoCliente dados = new DAL.CartaoCliente();
            DataSet ds = new DataSet();

            try
            {
                ds = dados.CarregaCartao(objCartao);

                objCartao.Cliente_Clicodigo = Convert.ToInt32(ds.Tables[0].Rows[0]["CLICODIGO"].ToString());
                objCartao.Cliente_NumCartao = ds.Tables[0].Rows[0]["NUMERO_CARTAO"].ToString();
                objCartao.Cliente_DtValidadeCartao = ds.Tables[0].Rows[0]["DATA_VALIDADE"].ToString();
                objCartao.Cliente_CVVCartao = ds.Tables[0].Rows[0]["CVV"].ToString();
                objCartao.Num_Retorno = ds.Tables[0].Rows[0]["RETORNO"].ToString();

            }
            catch (Exception ex)
            {
                string text = ex.Message;
            }
            finally
            { };

            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }


        #endregion
    }
}