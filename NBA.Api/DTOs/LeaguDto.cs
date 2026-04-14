namespace NBA.Api.DTOs
{
    public class LeaguDto
    {
        public long Leagueid { get; set; }

        public string Name { get; set; } = null!;

        public long Commissioner { get; set; }

        public string Seasonyear { get; set; } = null!;

        public int? Weeksforseason { get; set; }

        public int? Transactionlimit { get; set; }

        public bool? Autostart { get; set; }

        public int? Typetransactionlimits { get; set; }

        public int? Typeleague { get; set; }

        public int? Draftstyle { get; set; }

        public long? Statsvalueid { get; set; }
    }
}
