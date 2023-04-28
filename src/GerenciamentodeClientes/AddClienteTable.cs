using FluentMigrator;

namespace GerenciamentodeClientes
{
    [Migration(20230427101000)]
    public class AddClienteTable : Migration
    {
        public override void Up()
        {
            Create.Table("cliente")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("nome").AsString().NotNullable()
                .WithColumn("cpf").AsString().NotNullable()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("data_de_nascimento").AsDateTime().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("cliente");
        }
    }
}


