using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace GerenciamentodeClientes
{
    public class Pessoa
    {
        public const int valorInicialID = 0;
        public const int incrementoID = 1;
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string DatadeNascimento { get; set; }
        public string CPF { get; set; }

        public static int ReferenciaID = valorInicialID;
        public static int GerarID()
        {
            return Pessoa.ReferenciaID += incrementoID;
        }


    }

}
