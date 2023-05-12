using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Infraestrutura
{
    [Migration(20230511041200)]
    public class AddClienteTable : Migration
    {
        public override void Up()
        {
            Create.Table("Cliente")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("Nome").AsString().NotNullable()
                .WithColumn("CPF").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("DataDeNascimento").AsDateTime().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Cliente");
        }
    }
}


