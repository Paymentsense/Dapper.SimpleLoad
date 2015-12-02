using System;
using System.Data.SqlClient;
using Dapper.SimpleSave;
using NUnit.Framework;

namespace Dapper.SimpleLoad.Tests
{
    [TestFixture]
    public class InheritanceTests : BaseTests
    {
        [Table("[test].[A]")]
        public class A
        {
            [PrimaryKey]
            public int AKey {get; set;}
        }

        public class B : A
        {
        }

        [Table("[test].[C]")]
        public class C
        {
            [PrimaryKey]
            public int? CKey { get; set; }

            [Column("BKey")]
            [ManyToOne]
            public B Bobject { get; set; }
        }

        [Test]
        public void querying_class_referencing_subclass_retrieves_children()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    var results = connection.AutoQuery<C, B>(new { CKey = 1 });
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }
        }
    }
}
