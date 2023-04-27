namespace GerenciamentodeClientes
{
    public class ClienteListSingleton
    {
        private static ClienteListSingleton instancia;
        private List<Pessoa> clienteList;
        private ClienteListSingleton()
        {
            clienteList = new List<Pessoa>();
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
        public List<Pessoa> ClienteList 
        { 
          get { return clienteList; } 
          set { clienteList = value; }
        }
    }
}
