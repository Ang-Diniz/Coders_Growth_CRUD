using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentodeClientes
{
    public class ClienteListSingleton
    {
        private static ClienteListSingleton instancia;
        private List<Cliente> clienteList;
        private ClienteListSingleton()
        {
            clienteList = new List<Cliente>();
        }
        public static ClienteListSingleton Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ClienteListSingleton();
                }
                return instancia;
            }
        }
        public List<Cliente> ClienteList 
        { 
          get { return clienteList; } 
          set { clienteList = value; }
        }
    }
}
