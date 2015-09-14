using System;
using System.Data;

namespace Dapper.SimpleLoad
{
    public abstract class BaseCustomQueryDapperRepository
    {
        protected readonly IDbConnectionFactory _dbConnectionFactory;

        protected BaseCustomQueryDapperRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        protected TResult Execute<TResult>(Func<IDbConnection, TResult> work)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                return work(connection);
            }
        }

        protected void Execute(Action<IDbConnection> work)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                work(connection);
            }
        }
    }
}
