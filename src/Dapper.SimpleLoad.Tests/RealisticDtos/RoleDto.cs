﻿using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[user].ROLE_MST")]
    [ReferenceData]
    public class RoleDto
    {
        [PrimaryKey]
        public int? RoleKey { get; set; }

        public string Name { get; set; }
    }
}
