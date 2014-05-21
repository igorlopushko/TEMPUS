namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDataBaseGeneratedKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserMoods", "UserId", "dbo.Users");
            DropIndex("dbo.UserMoods", new[] { "UserId" });
            DropTable("dbo.UserMoods");
            DropForeignKey("dbo.ProjectRoleRelations", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoleRelations", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProjectRoleRelations", "ProjectId", "dbo.Projects");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Projects");
            AlterColumn("dbo.Users", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Projects", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Projects", "Id");

            CreateTable(
                "dbo.UserMoods",
                c => new
                {
                    UserId = c.Guid(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Rate = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.Date })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            AddForeignKey("dbo.ProjectRoleRelations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoleRelations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectRoleRelations", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectRoleRelations", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.UserRoleRelations", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProjectRoleRelations", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserMoods", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.Projects");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Projects", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Projects", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            AddForeignKey("dbo.ProjectRoleRelations", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoleRelations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectRoleRelations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserMoods", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
