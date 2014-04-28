namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFeelingsFromUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Feelings");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Feelings", c => c.String());
        }
    }
}
