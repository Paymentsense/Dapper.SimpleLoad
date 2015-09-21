using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Dapper.SimpleSave;
using Newtonsoft.Json;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[user].USER_MST")]
    class UserWithDepartmentsDto
    {
        public UserWithDepartmentsDto()
        {
            Department = new List<SimpleDepartmentDto>();
        }

        [PrimaryKey]
        public int? UserKey { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ManyToMany("[user].USER_DEPARTMENT_LNK")]
        public IList<SimpleDepartmentDto> Department { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
