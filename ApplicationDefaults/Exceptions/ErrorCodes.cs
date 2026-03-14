
namespace ApplicationDefaults.Exceptions
{
    public static class ErrorCodes
    {

        #region Generic 
        public const string DataBaseRecordNotFound = "5656-DATABASERECORDNOTFOUND";
        public const string EnumTypeDoesNotExist = "5656-ENUMTYPEDOESNOTEXIST";
        #endregion

        #region Specific
        public const string TradeCantBeExecuted = "5656-TRADECANTBEEXECUTED";
        public const string MaxCenterLimitReached = "5656-MAXCENTERLIMITREACHED";
        public const string TeamMaxPlayersReached = "5656-TEAMMAXPLAYERSREACHED";
        #endregion

    }
}
