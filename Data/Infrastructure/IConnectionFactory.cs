using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Infrastructure
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection GetConnection { get; }

        IDbConnection CreateDbConnection(DbConnectionString dbConnectionString);
    }

    public enum DbConnectionString
    {
        LocaDb,
        Connection163,
        Connection100,
        ConnectionLocal
    }
}
