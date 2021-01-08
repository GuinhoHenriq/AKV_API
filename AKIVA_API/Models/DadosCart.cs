using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AKIVA_API.Models
{
    public class DadosCart : Models.CartaoCli
    {
        
        public DadosCart(int cliCodigo)
        {
            this.Cliente_Clicodigo = cliCodigo;
        }

        DadosCart()
        { }
    }
}