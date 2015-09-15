﻿using System.Collections.Generic;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{

    [Table("[user].DEPARTMENT_MST")]
    [ReferenceData]
    public class DepartmentDto
    {
        [PrimaryKey]
        public int? DepartmentKey { get; set; }

        public string Name { get; set; }

        [OneToMany]
        public IList<GroupDto> Groups { get; set; } 
    }
}
