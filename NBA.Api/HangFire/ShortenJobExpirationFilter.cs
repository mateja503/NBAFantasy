using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace NBA.Api.HangFire
{
    public class ShortenJobExpirationFilter : JobFilterAttribute, IApplyStateFilter
    {
        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromDays(14);
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction) { }
    }
}
