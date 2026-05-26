using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Data.Redis.Entities
{
    public class PlayerShort
    {
        public long? PlayerId { get; set; } = null;
        public string? FullName { get; set; } = null;
        public string? Position { get; set; } = null;
    }
}
