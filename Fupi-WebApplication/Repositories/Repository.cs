using Fupi_WebApplication.DAL;
using Fupi_WebApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fupi_WebApplication.Repositories
{
    public class Repository : IRepository
    {
        private FupiAppContext _keyGen;
        public Repository(FupiAppContext keyGen)
        {
            this._keyGen = keyGen;
        }

        public async Task AddUrl(FupiModel model)
        {
            if(model != null)
            {
                await this._keyGen.FupiModel.AddAsync(model);
            }
        }

        public async Task<FupiModel> GetFupiUrl(Expression<Func<FupiModel, bool>> expression)
        {
            if(expression != null)
            {
                return await this._keyGen.FupiModel.Where(expression).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<IEnumerable<FupiModel>> GetFupiUrls(Expression<Func<FupiModel, bool>> expression = null)
        {
            if (expression != null)
            {
                return await this._keyGen.FupiModel.Where(expression).ToListAsync();
            }
            else
            {
                return await this._keyGen.FupiModel.ToListAsync();
            }
        }

        public async Task Update(FupiModel model)
        {
            FupiModel _model = await this._keyGen.FupiModel.Where(c => c.Id == model.Id).FirstOrDefaultAsync();

            if (_model != null)
            {
                _model = model;
            }
        }
    }
}
