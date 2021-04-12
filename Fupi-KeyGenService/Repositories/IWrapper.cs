using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fupi_KeyGenService.Repositories
{
    public interface IWrapper
    {
        public IRepository Repository { get; }
        Task SaveAsync();
    }
}
