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
            var connection = _dbConnectionFactory.GetConnection();
            MaybeOpenConnection(connection);
            return work(connection);
        }

        protected void Execute(Action<IDbConnection> work)
        {
            var connection = _dbConnectionFactory.GetConnection();
            MaybeOpenConnection(connection);
            work(connection);
        }

        private void MaybeOpenConnection(IDbConnection connection)
        {
            switch (connection.State)
            {
                case ConnectionState.Closed:
                    connection.Open();
                    break;

                case ConnectionState.Broken:
                    connection.Close();
                    connection.Open();
                    break;

                case ConnectionState.Open:
                case ConnectionState.Connecting:
                case ConnectionState.Executing:
                case ConnectionState.Fetching:
                default:
                    //  Do nothing - connection is already open
                    break;
            }
        }
    }
}
