using System.Text;

namespace Dapper.SimpleLoad
{
    public interface IQuery
    {
        string Sql { get; }
        string SplitOn { get; }
    }
}