namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDataBaseGeneratedKeyInProjectRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectRoleRelations", "ProjectRoleId", "dbo.ProjectRoles");
            DropPrimaryKey("dbo.ProjectRoles");
            AlterColumn("dbo.ProjectRoles", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ProjectRoles", "Id");
            AddForeignKey("dbo.ProjectRoleRelations", "ProjectRoleId", "dbo.ProjectRoles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectRoleRelations", "ProjectRoleId", "dbo.ProjectRoles");
            DropPrimaryKey("dbo.ProjectRoles");
            AlterColumn("dbo.ProjectRoles", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.ProjectRoles", "Id");
            AddForeignKey("dbo.ProjectRoleRelations", "ProjectRoleId", "dbo.ProjectRoles", "Id", cascadeDelete: true);
        }
    }
}
