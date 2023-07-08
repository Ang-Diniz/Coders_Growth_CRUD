using LinqToDB.Mapping;

namespace Dominio
{
    public class Cliente
    {
        public const int valorMinimoIdade = 18;

        [PrimaryKey, Identity]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string CPF { get; set; }
    }
}
