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
using System.Configuration;
using Newtonsoft.Json;

namespace AKV_API.Controllers
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

                objCartao.Cliente_NumCartao = (DecryptString(objCartao.Cliente_NumCartao));
                objCartao.Cliente_CVVCartao = (DecryptString(objCartao.Cliente_CVVCartao));
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

        [HttpGet]
        public HttpResponseMessage GetDadosCart(int cliCodigo)
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

                objCartao.Cliente_NumCartao = (DecryptString(objCartao.Cliente_NumCartao));
                objCartao.Cliente_CVVCartao = (DecryptString(objCartao.Cliente_CVVCartao));

                JsonConvert.SerializeObject(objCartao);

                return Request.CreateResponse(HttpStatusCode.OK, objCartao);
            }
            catch (Exception ex)
            {
                string text = ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, text);
            }
            finally
            { };
        }

        #region Método que Decriptografa as informações do BD
        
        public static string DecryptString(string code)
        {
            byte[] text = FromHex(code);
            byte[] key = FromHex(ConfigurationManager.AppSettings.Get("KeyDecrypt"));
           

            //return BitConverter.ToString(Encoding.Default.GetBytes(decrypt(text, key)));
            return decrypt(text, key);
        }

        public static byte[] FromHex(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        public static byte[] FromText(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i, 1), 16);
            }
            return raw;
        }

        public static String decrypt(byte[] input, byte[] key)
        {
            byte[] data = input;
            String decrypted;

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = key;
                rijAlg.Mode = CipherMode.ECB;
                rijAlg.BlockSize = 128;
                rijAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, null);
                using (MemoryStream msDecrypt = new MemoryStream(data))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decrypted = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return decrypted.ToString();
        }

    }
        #endregion
}