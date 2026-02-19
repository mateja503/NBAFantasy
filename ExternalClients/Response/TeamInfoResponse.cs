using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalClients.Response
{
    public record TeamInforResponse
    {
        public long id { get; init; }
        public string conference { get; init; }
        public string division { get; init; }
        public string city { get; init; }
        public string name { get; init; }
        public string full_name { get; init; }
        public string abbreviation { get; init; }
    }
}
