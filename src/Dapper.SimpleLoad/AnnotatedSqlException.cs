using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dapper.SimpleLoad
{
    [Serializable]
    public class AnnotatedSqlException : Exception, ISerializable
    {
        public AnnotatedSqlException(SqlException source, string sql, string splitOn, object parameters) : base(
                source.Message
                + Environment.NewLine
                + "For SQL: " + sql
                + Environment.NewLine
                + "Split On: " + splitOn
                + Environment.NewLine
                + "Parameters: " + JsonConvert.SerializeObject(parameters),
            source)
        {
            Sql = sql;
            SplitOn = splitOn;
            Parameters = parameters;
        }

        protected AnnotatedSqlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Sql = info.GetString("_Sql");
            SplitOn = info.GetString("_SplitOn");
            Parameters = JsonConvert.DeserializeObject(info.GetString("_Parameters"));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("_Sql", Sql);
            info.AddValue("_SplitOn", SplitOn);
            info.AddValue("_Parameters", JsonConvert.SerializeObject(Parameters));
        }

        public string Sql { get; private set; }
        public string SplitOn { get; private set; }
        public object Parameters { get; private set; }
    }
}
