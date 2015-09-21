using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[user].DEPARTMENT_MST")]
    [ReferenceData]
    public class SimpleDepartmentDto
    {
        [PrimaryKey]
        public int? DepartmentKey { get; set; }

        public string Name { get; set; }
    }
}
