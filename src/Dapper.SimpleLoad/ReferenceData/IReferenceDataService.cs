using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.SimpleLoad.ReferenceData
{
    public interface IReferenceDataService
    {
        IEnumerable<dynamic> GetReferenceData(string typeKey);

        IEnumerable<TResult> GetReferenceData<TResult>();
        
        IEnumerable<dynamic> GetReferenceData<TColumnValue>(string typeKey, string columnName, TColumnValue matchingColumnValue);

        IEnumerable<TResult> GetReferenceData<TResult, TColumnValue>(string columnName, TColumnValue matchingColumnValue);
    }
}
