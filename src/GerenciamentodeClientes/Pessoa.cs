using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace GerenciamentodeClientes
{
    public class Pessoa
    {
        public const int valorInicialId = 0;
        public const int incrementoId = 1;
        public const int valorMinimoIdade = 18;
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string CPF { get; set; }

        //public static int referenciaId = valorInicialId;
        //public static int GerarID()
        //{
        //    return Pessoa.referenciaId += incrementoId;
        //}
    }
}
