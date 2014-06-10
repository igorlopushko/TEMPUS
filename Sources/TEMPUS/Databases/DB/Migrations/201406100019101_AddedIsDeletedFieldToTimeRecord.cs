namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsDeletedFieldToTimeRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeRecords", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeRecords", "IsDeleted");
        }
    }
}
