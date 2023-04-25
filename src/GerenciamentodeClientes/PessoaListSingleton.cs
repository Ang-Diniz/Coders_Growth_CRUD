using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentodeClientes
{
    public class PessoaListSingleton
    {
        private static PessoaListSingleton instancia;
        private List<Pessoa> pessoaList;
        private PessoaListSingleton()
        {
            pessoaList = new List<Pessoa>();
        }
        public static PessoaListSingleton Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PessoaListSingleton();
                }
                return instancia;
            }
        }
        public List<Pessoa> PessoaList 
        { 
          get { return pessoaList; } 
          set { pessoaList = value; }
        }
    }
}
