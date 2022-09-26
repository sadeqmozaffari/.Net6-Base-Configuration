using Microsoft.Extensions.Configuration;
using App.DataLayer.Contracts;
using App.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDBContext _Context { get; }
       // private IMapper _mapper;
      
        private readonly IConfiguration _configuration;

        public UnitOfWork(AppDBContext context,/* IMapper mapper,*/ IConfiguration configuration)
        {
            _Context = context;
            //_mapper = mapper;
            _configuration = configuration;
        }

        public IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class
        {
            IBaseRepository<TEntity> repository = new BaseRepository<TEntity, AppDBContext>(_Context);
            return repository;
        }

    
        public async Task Commit()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
