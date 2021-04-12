using Fupi_KeyGenService.DAL;
using Fupi_KeyGenService.DTOs;
using Fupi_KeyGenService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fupi_KeyGenService.Repositories
{
    public class Repository : IRepository
    {
        private KeyGenContext keyGenContext;

        public Repository(KeyGenContext genContext)
        {
            this.keyGenContext = genContext;
        }

        public async Task AddKey(KeyGenModel keys)
        {
            if (keys != null)
            {
                await this.keyGenContext.KeyGenModel.AddAsync(keys);
            }
        }

        public async Task<LinkedList<KeyGenDto>> GetKeys(Expression<Func<KeyGenModel, bool>> expression = null)
        {
            if (expression != null)
            {
                return new LinkedList<KeyGenDto>(await this.keyGenContext.KeyGenModel.Where(expression).Select(v => new KeyGenDto() { Proxy = v.Proxy, CreatedTime = v.CreatedTime }).ToListAsync());
            }
            else
            {
                return new LinkedList<KeyGenDto>(await this.keyGenContext.KeyGenModel.Select(v => new KeyGenDto() { Proxy = v.Proxy, CreatedTime = v.CreatedTime }).ToListAsync());
            }
        }

        public async Task<bool> IsKeyExisting(Expression<Func<KeyGenModel, bool>> expression)
        {
            if (expression != null)
            {
                return await this.keyGenContext.KeyGenModel.Where(expression).AnyAsync();
            }
            return false;
        }

        public void Update(KeyGenDto key)
        {
            KeyGenModel keyGen = this.keyGenContext.KeyGenModel.Where(v => v.Proxy == key.Proxy).FirstOrDefault();

            if (keyGen != null)
            {
                keyGen.IsUtilized = true;
            }
        }
    }
}
