using FluentMigrator;
using FluentMigrator.SqlServer;

namespace GerenciamentodeClientes
{
    [Migration(20230427101000)]
    public class AddClienteTable : Migration
    {
        public override void Up()
        {
            Create.Table("clientes")
                .WithColumn("id").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("nome").AsString().NotNullable()
                .WithColumn("cpf").AsString().NotNullable()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("data_de_nascimento").AsDateTime().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("clientes");
        }
    }
}


