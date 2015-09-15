using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;
using NUnit.Framework;

namespace Dapper.SimpleLoad.Tests
{
    [TestFixture]
    public class QueryGenerationTests
    {
        public void generates_correct_query_for_complex_types()
        {
            //var logger = 
            //SimpleSaveExtensions.Logger = //    TODO

            try
            {
                using (var connection = new SqlConnection())
                {
                    //connection
                    //    .AutoQuery
                    //    <ApplicationOpportunitiesDto, ApplicationOpportunityLocationDto, OpportunityDto, OfferVersionDto
                    //        >(new
                    //        {
                    //            ApplicationKey = 1
                    //        });
                }
            }
            catch (SqlException)
            {
                //  Don't care.
            }

            //  TODO: check the script
            //return Query<ApplicationOpportunitiesDto, ApplicationOpportunityLocationDto, OpportunityDto, OfferVersionDto>
        }
    }
}
