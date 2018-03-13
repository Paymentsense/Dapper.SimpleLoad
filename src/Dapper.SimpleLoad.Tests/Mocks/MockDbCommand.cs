using System;
using System.Data;

namespace Dapper.SimpleLoad.Tests.Mocks
{
    public class MockDbCommand : IDbCommand
    {
        public MockDbCommand()
        {
            Parameters = new MockDataParameterCollection();
        }

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }

        public IDataParameterCollection Parameters { get; private set; }

        public UpdateRowSource UpdatedRowSource { get; set; }

        public void Cancel()
        {
        }

        public IDbDataParameter CreateParameter()
        {
            return new MockDbParameter();
        }

        public void Dispose()
        {
        }

        public int ExecuteNonQuery()
        {
            throw new MockDbQueryExecutedException();
        }

        public IDataReader ExecuteReader()
        {
            throw new MockDbQueryExecutedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new MockDbQueryExecutedException();
        }

        public object ExecuteScalar()
        {
            throw new MockDbQueryExecutedException();
        }

        public void Prepare()
        {
        }
    }
}
