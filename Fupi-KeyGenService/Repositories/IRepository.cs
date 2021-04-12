using Fupi_KeyGenService.DTOs;
using Fupi_KeyGenService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fupi_KeyGenService.Repositories
{
    public interface IRepository
    {
        Task<LinkedList<KeyGenDto>> GetKeys(Expression<Func<KeyGenModel, bool>> expression = null);
        Task AddKey(KeyGenModel keys);
        void Update(KeyGenDto key);

        Task<bool> IsKeyExisting(Expression<Func<KeyGenModel, bool>> expression);
    }
}
