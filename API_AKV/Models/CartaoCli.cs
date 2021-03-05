using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AKV_API.Models
{
    public class CartaoCli
    {
        #region Informação da classe
        /* 
     *      Autor: Guilherme Henrique - 08-01-2021
     *      Obs.: Essa classe representa o modelo da tabela TB_DADOS_CARTAO_COLETADO
     *      Os dados da tabela representam os dados do cartao do cliente
     */
        #endregion

            public int Cliente_Clicodigo { get; set; }
            public string Cliente_NumCartao { get; set; }
            public string Cliente_DtValidadeCartao { get; set; }
            public string Cliente_CVVCartao { get; set; }
            public string Num_Retorno { get; set; }

            
      public CartaoCli(int cliCodigo, string NumCart,string validadeCart, string CVV, string retorno)
        {
            this.Cliente_NumCartao = NumCart;
            this.Cliente_DtValidadeCartao = validadeCart;
            this.Cliente_CVVCartao = CVV;
            this.Cliente_Clicodigo = cliCodigo;
            this.Num_Retorno = retorno;
        }

        public CartaoCli()
        { }
        
       
    }
}