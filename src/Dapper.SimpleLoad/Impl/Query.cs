using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.SimpleLoad.Impl
{
    public class Query : IQuery
    {
        public string Sql { get; set; }
        public string SplitOn { get; set; }
    }
}
