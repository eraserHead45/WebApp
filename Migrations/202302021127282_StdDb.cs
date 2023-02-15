namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StdDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        RollNo = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Department = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.RollNo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
