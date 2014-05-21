namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldToUserAndProjectEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "IsDeleted");
            DropColumn("dbo.Users", "IsDeleted");
        }
    }
}
