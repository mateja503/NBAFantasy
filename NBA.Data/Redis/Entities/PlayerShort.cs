using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Data.Redis.Entities
{
    public class PlayerShort
    {
        public long? Playerid { get; set; } = null;
        public string? Fullname { get; set; } = null;
        public string? Position { get; set; } = null;
        public bool? Isdrafted { get; set; } = null;
    }
}
