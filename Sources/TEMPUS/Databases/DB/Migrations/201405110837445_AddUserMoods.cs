namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserMoods : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Moods", newName: "UserMoods");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserMoods", newName: "Moods");
        }
    }
}
