namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Moods",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Rate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.Date })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Moods", "UserId", "dbo.Users");
            DropIndex("dbo.Moods", new[] { "UserId" });
            DropTable("dbo.Moods");
        }
    }
}
