using Fupi_KeyGenService.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fupi_KeyGenService.Repositories
{
    public class RepoWrapper: IWrapper
    {
        private IRepository _repo { get; set; }
        private KeyGenContext context;

        public IRepository Repository
        {
            get
            {
                if (this._repo == null)
                {
                    this._repo = new Repository(this.context);
                }
                return this._repo;
            }
        }

        public RepoWrapper(KeyGenContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
