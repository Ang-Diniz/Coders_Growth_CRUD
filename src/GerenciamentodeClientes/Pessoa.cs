using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace GerenciamentodeClientes
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string DatadeNascimento { get; set; }
        public string CPF { get; set; }

        public static int ReferenciaID = 0;
        public static int GerarID()
        {
            return Pessoa.ReferenciaID += 1;
        }


    }

}
