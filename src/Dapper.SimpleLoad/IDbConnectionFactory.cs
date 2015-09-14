using System.Data;

namespace Dapper.SimpleLoad
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
