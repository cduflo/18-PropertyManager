namespace PropertyManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Auth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Properties", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Properties", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Properties", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
