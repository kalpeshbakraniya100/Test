using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int Id);
        int AddOrUpdate(int id, TEntity entity);
        Task<int> AddOrUpdateAsync(int id, TEntity entity);
        void Delete(TEntity entity);
        void Commit();
        IEnumerable<TEntity> GetDataBySP(string sp_Name);
    }
}
