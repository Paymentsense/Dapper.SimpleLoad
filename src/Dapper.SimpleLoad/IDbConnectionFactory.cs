using System;
using System.Data;

namespace Dapper.SimpleLoad
{
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection GetConnection();
    }
}
