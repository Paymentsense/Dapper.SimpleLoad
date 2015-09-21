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
        }

        [Test]
        public void generates_correct_query_with_custom_where_condition()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    var results = connection.AutoQuery<PhoneNumberDto, CountryDto>(
                        new[] {"n", "c"},
                        "[c].[TelephoneCountryCode] = @CountryCode AND [n].[PhoneNumber] = @Number",
                        new
                        {
                            CountryCode = "44",
                            Number = "7779123456"
                        });
                }
            }
            catch (InvalidOperationException)
            {
                //  Don't care - connection not open.
            }
        }

        [Test]
        public void generates_correct_query_for_many_to_many_relationship()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    var results = connection.AutoQuery<UserWithDepartmentsDto, SimpleDepartmentDto>(
                        new
                        {
                            UserKey = 1
                        });
                }
            }
            catch (InvalidOperationException)
            {
                //  Don't care - connection not open.
            }
        }
    }
}
