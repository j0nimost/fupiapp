using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fupi_WebApplication.Repositories
{
    public interface IWrapper
    {
        public IRepository Repo { get; }
        Task SaveAsync();
    }
}
