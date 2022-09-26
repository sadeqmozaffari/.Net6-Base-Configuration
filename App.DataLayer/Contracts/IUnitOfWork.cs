using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DataLayer.Contracts
{
    public interface IUnitOfWork
    {
        IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class;
       
        AppDBContext _Context { get; }
        Task Commit();
    }
}
