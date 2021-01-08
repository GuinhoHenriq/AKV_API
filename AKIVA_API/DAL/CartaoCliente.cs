using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using AKIVA_API.Models;
using System.Configuration;

namespace AKIVA_API.DAL
{
    public class CartaoCliente : CartaoCli
    {

        public DataSet GravaCartao(CartaoCli objCartao)
        {
            System.Data.SqlClient.SqlConnection conexao = new SqlConnection(ConfigurationManager.AppSettings.Get("Conn17").ToString());
            SqlCommand comando = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da;

            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "STP_GRAVA_INFORMACAO_CARTAO";
            comando.CommandTimeout = 3000;
            comando.Parameters.Add("@CLICODIGO", SqlDbType.Int).Value = objCartao.Cliente_Clicodigo;
            comando.Parameters.Add("@NUM_CARTAO", SqlDbType.VarChar).Value = objCartao.Cliente_NumCartao;
            comando.Parameters.Add("@DT_VALIDADE_CART", SqlDbType.VarChar).Value = objCartao.Cliente_DtValidadeCartao;
            comando.Parameters.Add("@CVV", SqlDbType.VarChar).Value = objCartao.Cliente_CVVCartao;
            comando.Parameters.Add("@CODIGO_RET", SqlDbType.VarChar).Value = objCartao.Num_Retorno;
            da = new SqlDataAdapter(comando);
            da.Fill(ds, "Dados");
            conexao.Close();
            return ds;
        }

        public DataSet CarregaCartao(CartaoCli objCartao)
        {
            System.Data.SqlClient.SqlConnection conexao = new SqlConnection(ConfigurationManager.AppSettings.Get("Conn17"));
            SqlCommand comando = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da;

            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "STP_CARREGA_INFORMACAO_CARTAO";
            comando.CommandTimeout = 3000;
            comando.Parameters.Add("@CLICODIGO", SqlDbType.Int).Value = objCartao.Cliente_Clicodigo;
            da = new SqlDataAdapter(comando);
            da.Fill(ds, "Dados");
            conexao.Close();

            return ds;
        }            


    }
}