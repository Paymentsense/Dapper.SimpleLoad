using System;

namespace Dapper.SimpleLoad.Tests
{
    public abstract class BaseTests
    {
        protected void CheckException(InvalidOperationException ioe)
        {
            if (!ioe.Message.Contains("The ConnectionString property has not been initialized."))
            {
                throw new BadTimesException(
                    "InvalidOperationException due to legitimate test failure: " + ioe.Message,
                    ioe);
            }
        }
    }
}
