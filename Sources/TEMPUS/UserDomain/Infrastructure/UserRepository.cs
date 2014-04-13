using System;
using System.Data;
using System.Data.SqlClient;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.Infrastructure.Data;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    public class UserRepository : Repository<User, UserId>, IUserRepository
    {
        private IUserQueryService _userQueryService;

        //TODO: change to configuration interface
        private string _dbConnectionString = "Data Source=.;Integrated Security=SSPI;Initial Catalog=TEMPUS;Persist Security Info=True";

        public UserRepository(IEventStore eventStore, IUserQueryService userQueryService)
            : base(eventStore)
        {
            _userQueryService = userQueryService;
        }

        /// <summary>
        /// Get user aggregate root by identity.
        /// </summary>
        /// <param name="id">user identity</param>
        /// <returns></returns>
        public override User Get(UserId id)
        {
            var user = _userQueryService.GetUserById(id);

            return new User(id, user.FirstName, user.LastName, user.Login, user.Password, user.Age, user.Image,
                user.Phone);
        }

        /// <summary>
        /// Save user aggregate root state.
        /// </summary>
        /// <param name="root">aggregate root state object</param>
        public override void Save(User root)
        {
            if (root.IsNew)
            {
                DbConnector.Execute(new SqlConnection(_dbConnectionString), BuildCreateCommand(root));
            }
            else
            {
                DbConnector.Execute(new SqlConnection(_dbConnectionString), BuildUpdateCommand(root));
            }
        }

        private SqlCommand BuildCreateCommand(User user)
        {
            var cmd = new SqlCommand
                {
                    CommandText = "sp_CreateUser", 
                    CommandType = CommandType.StoredProcedure
                };

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) {Value = user.Id.Id});
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = user.FirstName });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = user.LastName });

            return cmd;
        }

        private SqlCommand BuildUpdateCommand(User user)
        {
            var cmd = new SqlCommand
                {
                    CommandText = "sp_UpdateUser", 
                    CommandType = CommandType.StoredProcedure
                };

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = user.Id.Id });
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = user.FirstName });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = user.LastName });

            return cmd;
        }

        private User BuildObjectFromDataSet(DataSet data)
        {
            throw new NotImplementedException();
        }
    }
}