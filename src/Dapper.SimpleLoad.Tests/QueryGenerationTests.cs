using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleLoad.Tests.RealisticDtos;
using Dapper.SimpleSave;
using NUnit.Framework;

namespace Dapper.SimpleLoad.Tests
{
    [TestFixture]
    public class QueryGenerationTests
    {
        [Test]
        public void generates_correct_query_for_complex_types()
        {
            //var logger = 
            //SimpleSaveExtensions.Logger = //    TODO

            try
            {
                using (var connection = new SqlConnection())
                {
                    connection
                        .AutoQuery
                        <PhoneNumberDto, CountryDto, CurrencyCodeDto>(new
                            {
                                PhoneNumberKey = 1
                            });
                }
            }
            catch (InvalidOperationException)
            {
                //  Don't care - connection not open.
            }

            //  TODO: check the script
            //return Query<ApplicationOpportunitiesDto, ApplicationOpportunityLocationDto, OpportunityDto, OfferVersionDto>
        }
    }
}
