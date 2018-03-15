using System;
using System.Collections.Generic;
using System.Data;

namespace Dapper.SimpleLoad.Tests.Mocks
{
    public class MockDbConnection : IDbConnection
    {
        public MockDbConnection()
        {
            Commands = new List<MockDbCommand>();
        }

        public IList<MockDbCommand> Commands { get; set; }

        public string FirstCommandText => Commands[0].CommandText;

        public string ConnectionString { get; set; }

        public int ConnectionTimeout => 0;

        public string Database => "TEST_DB";

        public ConnectionState State => ConnectionState.Open;

        public IDbTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return null;
        }

        public void ChangeDatabase(string databaseName)
        {
        }

        public void Close()
        {
        }

        public IDbCommand CreateCommand()
        {
            var command = new MockDbCommand
            {
                Connection = this
            };

            Commands.Add(command);

            return command;
        }

        public void Open()
        {
        }

        public void Dispose()
        {
        }
    }
}
