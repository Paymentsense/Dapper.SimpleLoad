namespace Dapper.SimpleLoad
{
    /// <summary>
    /// Defines configuration settings which alter SimpleLoad's behaviour. Currently all
    /// relates to logging and how SimpleLoad should handle long running queries, and queries
    /// that handle large results sets.
    /// </summary>
    public static class SimpleLoadConfiguration
    {
        private static int _rowCountWarningEmitThreshold;
        private static int _rowCountErrorEmitThreshold;
        private static int _rowCountExceptionThrowThreshold;

        private static long _queryDurationMillisWarningEmitThreshold;
        private static long _queryDurationMillisErrorEmitThreshold;
        private static long _queryDurationMillisExceptionThrowThreshold;

        static SimpleLoadConfiguration()
        {
            RowCountWarningEmitThreshold = 1000;
            RowCountErrorEmitThreshold = 2000;
            RowCountExceptionThrowThreshold = 0;    //  I.e., don't ever thrown an exception
            QueryDurationMillisWarningEmitThreshold = 2000;
            QueryDurationMillisErrorEmitThreshold = 4000;
            QueryDurationMillisExceptionThrowThreshold = 0;     //  I.e., don't ever thrown an exception
        }

        /// <summary>
        /// Number of rows in result set above which SimpleLoad will log a warning.
        /// Ignored if set to 0.
        /// </summary>
        public static int RowCountWarningEmitThreshold
        {
            get { return _rowCountWarningEmitThreshold; }
            set
            {
                value.IsGreaterThanOrEqualTo(nameof(RowCountWarningEmitThreshold), 0);
                _rowCountWarningEmitThreshold = value;
            }
        }

        /// <summary>
        /// Number of rows in result set above which SimpleLoad will log an error.
        /// Ignored if set to 0.
        /// </summary>
        public static int RowCountErrorEmitThreshold
        {
            get { return _rowCountErrorEmitThreshold; }
            set
            {
                value.IsGreaterThanOrEqualTo(nameof(RowCountErrorEmitThreshold), 0);
                _rowCountErrorEmitThreshold = value;
            }
        }

        /// <summary>
        /// Number of rows in result set above which SimpleLoad will throw an exception.
        /// Ignored if set to 0.
        /// </summary>
        public static int RowCountExceptionThrowThreshold
        {
            get { return _rowCountExceptionThrowThreshold; }
            set
            {
                value.IsGreaterThanOrEqualTo(nameof(RowCountExceptionThrowThreshold), 0);
                _rowCountExceptionThrowThreshold = value;
            }
        }

        /// <summary>
        /// Total duration of query and mapping above which SimpleLoad will log a warning.
        /// Ignored if set to 0.
        /// </summary>
        public static long QueryDurationMillisWarningEmitThreshold
        {
            get { return _queryDurationMillisWarningEmitThreshold; }
            set
            {
                value.IsGreaterThanOrEqualTo(nameof(QueryDurationMillisWarningEmitThreshold), 0);
                _queryDurationMillisWarningEmitThreshold = value;
            }
        }

        /// <summary>
        /// Total duration of query and mapping above which SimpleLoad will log an error.
        /// Ignored if set to 0.
        /// </summary>
        public static long QueryDurationMillisErrorEmitThreshold
        {
            get { return _queryDurationMillisErrorEmitThreshold; }
            set
            {
                value.IsGreaterThanOrEqualTo(nameof(QueryDurationMillisErrorEmitThreshold), 0);
                _queryDurationMillisErrorEmitThreshold = value;
            }
        }

        /// <summary>
        /// Total duration of query and mapping above which SimpleLoad will throw an exception.
        /// Ignored if set to 0.
        /// </summary>
        public static long QueryDurationMillisExceptionThrowThreshold
        {
            get { return _queryDurationMillisExceptionThrowThreshold; }
            set
            {
                value.IsGreaterThanOrEqualTo(nameof(QueryDurationMillisExceptionThrowThreshold), 0);
                _queryDurationMillisExceptionThrowThreshold = value;
            }
        }

        /// <summary>
        /// Indicates whether or not query parameters should be included in log output. Off by
        /// default and should not be switched on unless you are certain your query parameters
        /// do not contain sensitive information.
        /// </summary>
        public static bool IncludeParametersInLog { get; set; }

        /// <summary>
        /// Indicates whether or not query parameters should be included in exceptions. Off by
        /// default and should not be switched on unless you are certain your query parameters
        /// do not contain sensitive information.
        /// </summary>
        public static bool IncludeParametersInException { get; set; }
    }
}
