using Fupi_WebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fupi_WebApplication.Repositories
{
    public class RepoWrapper : IWrapper
    {
        private IRepository _repo;
        private FupiAppContext context;
        public IRepository Repo
        {
            get
            {
                if(_repo == null)
                {
                    this._repo = new Repository(context);
                }
                return this._repo;
            }
        }

        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }

        public RepoWrapper(FupiAppContext context)
        {
            this.context = context;
        }
    }
}
