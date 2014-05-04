namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRoleRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RolePermissions", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.RolePermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.RolePermissions", new[] { "Role_Id" });
            DropIndex("dbo.RolePermissions", new[] { "Permission_Id" });
            CreateTable(
                "dbo.UserRoleRelations",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            AddColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Login");
            DropColumn("dbo.Users", "Age");
            DropColumn("dbo.Users", "RoleId");
            DropTable("dbo.RolePermissions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Role_Id = c.Guid(nullable: false),
                        Permission_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Permission_Id });
            
            AddColumn("dbo.Users", "RoleId", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Login", c => c.String());
            DropForeignKey("dbo.UserRoleRelations", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoleRelations", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserRoleRelations", new[] { "RoleId" });
            DropIndex("dbo.UserRoleRelations", new[] { "UserId" });
            AlterColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.Users", "DateOfBirth");
            DropTable("dbo.UserRoleRelations");
            CreateIndex("dbo.RolePermissions", "Permission_Id");
            CreateIndex("dbo.RolePermissions", "Role_Id");
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RolePermissions", "Permission_Id", "dbo.Permissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RolePermissions", "Role_Id", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
