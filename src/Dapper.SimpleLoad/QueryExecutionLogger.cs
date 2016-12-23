using log4net;
using Newtonsoft.Json;
using System;

namespace Dapper.SimpleLoad
{
    public class QueryExecutionLogger
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(QueryExecutionLogger));

        private enum ThresholdBreach
        {
            None,
            Warn,
            Error,
            Throw
        }

        public static void Executing(IQuery query, object parameters)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info("Executing query:\r\n" + query.Sql);
                Log.Info("Split on: " + query.SplitOn);

                if (SimpleLoadConfiguration.IncludeParametersInLog)
                {
                    Log.Info("Parameters: " + JsonConvert.SerializeObject(parameters));
                }
            }
        }

        public static void Executed(IQuery query, object parameters, int rowCount, long startTime, long timeToFirstResult)
        {
            var totalTime = (DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerMillisecond;
            var breach = DecideOnBreachLevel(rowCount, totalTime);

            if (breach != ThresholdBreach.None)
            {
                var message = $@"RECEIVED {rowCount} ROWS IN QUERY RESULT SET IN {totalTime}ms FROM QUERY:
{query.Sql}
CALLING STACK TRACE:
{Environment.StackTrace}";

                if (SimpleLoadConfiguration.IncludeParametersInLog)
                {
                    message += Environment.NewLine
                        + "Parameters: "
                        + Environment.NewLine
                        + JsonConvert.SerializeObject(parameters);
                }

                switch (breach)
                {
                    case ThresholdBreach.Throw:
                        if (Log.IsFatalEnabled)
                        {
                            Log.Fatal(message);
                        }
                        throw new AnnotatedSqlException(message, query.Sql, query.SplitOn, parameters);  //  TODO: what exception to throw

                    case ThresholdBreach.Error:
                        if (Log.IsErrorEnabled)
                        {
                            Log.Error(message);
                        }
                        break;

                    case ThresholdBreach.Warn:
                    default:
                        if (Log.IsWarnEnabled)
                        {
                            Log.Warn(message);
                        }
                        break;
                }
            }
            else
            {
                if (Log.IsInfoEnabled)
                {
                    Log.Info(string.Format(
                        "Received {0} rows in query result set in {1}ms.",
                        rowCount,
                        totalTime));
                }
            }
        }

        private static ThresholdBreach DecideOnBreachLevel(int rowCount, long totalTime)
        {
            var breach = ThresholdBreach.None;

            if (BreachesThreshold(
                rowCount,
                SimpleLoadConfiguration.RowCountExceptionThrowThreshold,
                totalTime,
                SimpleLoadConfiguration.QueryDurationMillisExceptionThrowThreshold))
            {
                breach = ThresholdBreach.Throw;
            }
            else if (BreachesThreshold(
                rowCount,
                SimpleLoadConfiguration.RowCountErrorEmitThreshold,
                totalTime,
                SimpleLoadConfiguration.QueryDurationMillisExceptionThrowThreshold))
            {
                breach = ThresholdBreach.Error;
            }
            else if (BreachesThreshold(
                rowCount,
                SimpleLoadConfiguration.RowCountErrorEmitThreshold,
                totalTime,
                SimpleLoadConfiguration.QueryDurationMillisExceptionThrowThreshold))
            {
                breach = ThresholdBreach.Warn;
            }

            return breach;
        }

        private static bool BreachesThreshold(int rowCount, int rowCountThreshold, long totalTime, long totalTimeThreshold)
        {
            return ((rowCountThreshold > 0 && rowCount >= rowCountThreshold)
                    || (totalTimeThreshold > 0 && totalTime >= totalTimeThreshold));
        }
    }
}
