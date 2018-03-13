using System;
using System.Collections.Generic;
using System.Data;

namespace Dapper.SimpleLoad.Tests.Mocks
{
    public class MockDataParameterCollection : List<IDataParameter>, IDataParameterCollection
    {
        public object this[string parameterName] { get { return null; } set {} }

        public bool Contains(string parameterName)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(string parameterName)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(string parameterName)
        {
            throw new NotImplementedException();
        }
    }
}
