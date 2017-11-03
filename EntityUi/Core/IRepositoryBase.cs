using System;
using System.Collections.Generic;
using System.Data.Entity;
namespace EntityUi.Core
{
    public interface IRepositoryBase<TEntity, TContext>
     where TEntity : IDomainModelBase
     where TContext : DbContext

    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(int id);
        List<TEntity> List();
        void Update(TEntity entity);
    }
}
