using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;

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

                objCartao.Cliente_NumCartao = (Decrypt(objCartao.Cliente_NumCartao));
                objCartao.Cliente_CVVCartao = (Decrypt(objCartao.Cliente_CVVCartao));
            }
            catch (Exception ex)
            {
                string text = ex.Message;
            }
            finally
            { };

            string textjson = objCartao.Cliente_Clicodigo + "|" + objCartao.Cliente_NumCartao + "|" + objCartao.Cliente_DtValidadeCartao + "|" + objCartao.Cliente_CVVCartao + "|" + objCartao.Num_Retorno;
            

            return Request.CreateResponse(HttpStatusCode.OK, textjson);
        }

        #endregion

        #region Método que Decriptografa as informações do BD
        static string Decrypt(string Code)
        {
            Chilkat.Crypt2 crypt = new Chilkat.Crypt2();
            crypt.CryptAlgorithm = "AES";
            crypt.CipherMode = "ECB";
            crypt.KeyLength = 128;
            crypt.PaddingScheme = 0;
            crypt.EncodingMode = "HEX";

            //string keyHex = "4D43454E56444556";
            string keyHex = "34443433343534453536343434353536";
            crypt.SetEncodedKey(keyHex, "hex");

            string decStr = crypt.DecryptStringENC(Code);

            return decStr;
        }
        #endregion
    }
}