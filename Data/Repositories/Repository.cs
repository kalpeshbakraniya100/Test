using Dapper;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected IConnectionFactory _connectionFactory;

        public Repository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<TEntity> Get()
        {
            using var conn = _connectionFactory.GetConnection;
            return SimpleCRUD.GetList<TEntity>(conn);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            using var conn = _connectionFactory.GetConnection;
            return await SimpleCRUD.GetListAsync<TEntity>(conn);
        }

        public TEntity Get(int id)
        {
            using var conn = _connectionFactory.GetConnection;
            return SimpleCRUD.Get<TEntity>(conn, (object)id);
        }

        public async System.Threading.Tasks.Task<TEntity> GetAsync(TEntity entity)
        {
            if (entity == null) return null;
            using var conn = _connectionFactory.GetConnection;
            return await SimpleCRUD.GetAsync<TEntity>(conn, entity);
        }

        public int AddOrUpdate(int id, TEntity entity)
        {
            if (entity == null) return 0;
            try
            {
                using var conn = _connectionFactory.GetConnection;
                if (id <= 0)
                {
                    //TrySetProperty(entity, "CreatedDate", DateTime.Now);
                    //TrySetProperty(entity, "ModifiedDate", DateTime.Now);
                    return (int)SimpleCRUD.Insert(conn, entity);
                }
                else
                {
                    //TrySetProperty(entity, "ModifiedDate", DateTime.Now);
                    var rows = SimpleCRUD.Update(conn, entity);
                    if (rows > 0) return id;
                    return 0;
                }
            }
            catch (Exception ex) {
                _ = ex.Message; return 0; }

        }

        public async Task<int> AddOrUpdateAsync(int id, TEntity entity)
        {
            if (entity == null) return 0;
            try
            {
                using var conn = _connectionFactory.GetConnection;
                if (id <= 0)
                {
                    TrySetProperty(entity, "CreatedDate", DateTime.Now); // for servers not in Australia.
                    var res = await SimpleCRUD.InsertAsync(conn, entity);
                    return res ?? 0 ;
                }
                else
                {
                    var res = await SimpleCRUD.UpdateAsync(conn, entity);
                    if (res >= 0) return id;
                }
            }
            catch (Exception ex) {
                _ = ex.Message;
            }
            return 0;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) return;
            using var conn = _connectionFactory.GetConnection;
            try
            {
                SimpleCRUD.Delete(conn, entity);
            }
            catch (Exception ex) {
                _ = ex.Message;
            }
        }

        public IEnumerable<TEntity> GetDataBySP(string sp_name)
        {
            using var conn = _connectionFactory.GetConnection;
            return conn.Query<TEntity>(sp_name, commandType: CommandType.StoredProcedure);
        }

        //public IEnumerable<dynamic> GetDataBySPWithParam(string sp_name, FilterParam param)
        //{
        //    using (var conn = _connectionFactory.GetConnection)
        //    {
        //        var output = conn.Query<dynamic>(sp_name, param: (object)param, commandType: CommandType.StoredProcedure);
        //        return output;
        //    }
        //}

        private void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
                prop.SetValue(obj, value, null);
        }

        public void Commit()
        {
            // Nothing to do.
        }
    }
}


