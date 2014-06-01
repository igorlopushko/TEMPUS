namespace TEMPUS.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFTEToProjectRoleRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectRoleRelations", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProjectRoleRelations", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProjectRoleRelations", "FTE", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectRoleRelations", "FTE");
            DropColumn("dbo.ProjectRoleRelations", "EndDate");
            DropColumn("dbo.ProjectRoleRelations", "StartDate");
        }
    }
}
