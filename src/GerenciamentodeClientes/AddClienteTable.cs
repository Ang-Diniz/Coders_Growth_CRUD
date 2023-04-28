using FluentMigrator;
using System;
using System.Collections.Generic;

namespace GerenciamentodeClientes
{
    [Migration(20230427101000)]
    public class AddClienteTable : Migration
    {
        public override void Up()
        {
            Create.Table("Cliente")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
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


