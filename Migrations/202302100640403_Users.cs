namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Index = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                        ConfirmPassword = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Index);
            
            AddColumn("dbo.Students", "Department", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.Students", "Departemnet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Departemnet", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.Students", "Department");
            DropTable("dbo.UserDetails");
        }
    }
}
