using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.SimpleLoad.Tests.Mocks
{
    public class MockDataParameterCollection : List<IDataParameter>, IDataParameterCollection
    {
        public object this[string parameterName] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
