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
    public class RecebeInfoController : ApiController
    {

        #region Receber dados URA
        
        [HttpPost]
        public Models.CartaoCli PostDadosURA(int cliCodigo, string numCart, string validadeCart, string CVV, string retorno)
        {
            Models.CartaoCli objCartao = new Models.CartaoCli(cliCodigo, numCart, validadeCart, CVV, retorno);
            DAL.CartaoCliente dados = new DAL.CartaoCliente();

            

            try
            {
                dados.GravaCartao(objCartao);
            }
            catch (Exception ex)
            {
               string text = ex.Message;
            }
            finally 
            { };

            return objCartao;
        }
        #endregion

    }
}