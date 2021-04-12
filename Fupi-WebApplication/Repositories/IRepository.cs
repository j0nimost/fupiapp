using Fupi_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fupi_WebApplication.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<FupiModel>> GetFupiUrls(Expression<Func<FupiModel, bool>> expression = null);
        Task<FupiModel> GetFupiUrl(Expression<Func<FupiModel, bool>> expression);
        Task AddUrl(FupiModel model);
        Task Update(FupiModel model);
    }
}
