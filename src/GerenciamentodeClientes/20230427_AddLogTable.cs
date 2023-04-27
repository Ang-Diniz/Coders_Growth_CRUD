using FluentMigrator;

namespace GerenciamentodeClientes
{
    //internal class _20230427_AddLogTable
    [Migration(20230427083000)]
    public class AddLogTable : Migration
    {
        public override void Up()
        {
            Create.Table("Log")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Text").AsString();
        }
        public override void Down()
        {
            Delete.Table("Log");
        }
    }
}


