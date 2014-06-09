namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Effort = c.Double(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeRecords", "UserId", "dbo.Users");
            DropForeignKey("dbo.TimeRecords", "ProjectId", "dbo.Projects");
            DropIndex("dbo.TimeRecords", new[] { "ProjectId" });
            DropIndex("dbo.TimeRecords", new[] { "UserId" });
            DropTable("dbo.TimeRecords");
        }
    }
}
