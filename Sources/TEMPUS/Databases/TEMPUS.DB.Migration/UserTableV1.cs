using FluentMigrator;

namespace TEMPUS.DB.Migration
{
    /// <summary>
    /// Keep migration date in following format: YYYYMMDD
    /// </summary>
    [Migration(20140412)]
    public class UserTableV1 : FluentMigrator.Migration
    {
        public override void Down()
        {
            Execute.EmbeddedScript("User.drop.v1.sql");
            Execute.EmbeddedScript("sp_GetUserById.drop.v1.sql");
        }

        public override void Up()
        {
            Execute.EmbeddedScript("User.create.v1.sql");
            Execute.EmbeddedScript("sp_GetUserById.create.v1.sql");
        }
    }
}