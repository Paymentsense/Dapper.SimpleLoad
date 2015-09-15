﻿using System.Collections.Generic;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[user].[GROUP_MST]")]
    [ReferenceData]
    public class GroupDto
    {
        public GroupDto()
        {
            Roles = new List<RoleDto>();
        }

        [PrimaryKey]
        public int? GroupKey { get; set; }

        [ManyToMany("[user].[ROLE_GROUP_LNK]")]
        public IList<RoleDto> Roles { get; set; } 
    }
}
