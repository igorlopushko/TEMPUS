namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectAndRemovePermissions : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Permissions", newName: "Departments");
            CreateTable(
                "dbo.PpsClassifications",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 64),
                        Weight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProjectRoleRelations",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false),
                        ProjectRoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.ProjectRoleId, t.UserId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectRoles", t => t.ProjectRoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.ProjectRoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 64),
                        Description = c.String(),
                        ProjectOrderer = c.String(maxLength: 64),
                        RecievingOrganization = c.String(maxLength: 64),
                        Mandatory = c.Boolean(nullable: false),
                        DepartmentId = c.Guid(nullable: false),
                        PpsClassificationId = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.PpsClassifications", t => t.PpsClassificationId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.PpsClassificationId);
            
            CreateTable(
                "dbo.ProjectRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectRoleRelations", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProjectRoleRelations", "ProjectRoleId", "dbo.ProjectRoles");
            DropForeignKey("dbo.ProjectRoleRelations", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "PpsClassificationId", "dbo.PpsClassifications");
            DropForeignKey("dbo.Projects", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.ProjectRoles", new[] { "Name" });
            DropIndex("dbo.Projects", new[] { "PpsClassificationId" });
            DropIndex("dbo.Projects", new[] { "DepartmentId" });
            DropIndex("dbo.ProjectRoleRelations", new[] { "UserId" });
            DropIndex("dbo.ProjectRoleRelations", new[] { "ProjectRoleId" });
            DropIndex("dbo.ProjectRoleRelations", new[] { "ProjectId" });
            DropIndex("dbo.PpsClassifications", new[] { "Name" });
            DropTable("dbo.ProjectRoles");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectRoleRelations");
            DropTable("dbo.PpsClassifications");
            RenameTable(name: "dbo.Departments", newName: "Permissions");
        }
    }
}
