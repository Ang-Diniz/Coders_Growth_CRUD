using System;
using System.Collections.Generic;

namespace GerenciamentodeClientes
{
    public class Pessoa
    {
        public static int ReferenciaID = 0;
        public static int GerarID()
        {
            Pessoa.ReferenciaID = Pessoa.ReferenciaID + 1;
            return Pessoa.ReferenciaID;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string DatadeNascimento { get; set; }
        public string CPF { get; set; }


    }

}
