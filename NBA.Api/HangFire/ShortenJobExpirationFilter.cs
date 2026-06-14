using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace NBA.Api.HangFire
{
    public class ShortenJobExpirationFilter : JobFilterAttribute, IApplyStateFilter
    {
        // Draft jobs are short-lived; one day of history is plenty for diagnostics
        // and keeps the hangfire schema from growing unbounded.
        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromDays(1);
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction) { }
    }
}
