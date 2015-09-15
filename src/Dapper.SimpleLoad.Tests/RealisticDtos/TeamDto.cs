﻿using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[user].TEAM_MST")]
    [ReferenceData]
    public class TeamDto
    {
        [PrimaryKey]
        public int? TeamKey { get; set; }

        public string Name { get; set; }
    }
}
