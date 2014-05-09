using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMPUS.DB;
using TEMPUS.ProjectDomain.Services;

namespace TEMPUS.ProjectDomain.Infrastructure
{
    public class ProjectStorage : IProjectStorage<DB.Models.Project.Project>
    {
        private readonly DataContext _context;

        public ProjectStorage(DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this._context = context;
        }

        public DB.Models.Project.Project Get(BaseDomain.Messages.Identities.UserId id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DB.Models.Project.Project> Get(System.Linq.Expressions.Expression<Func<DB.Models.Project.Project, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Add(DB.Models.Project.Project entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DB.Models.Project.Project entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DB.Models.Project.Project aggregate)
        {
            throw new NotImplementedException();
        }
    }
}