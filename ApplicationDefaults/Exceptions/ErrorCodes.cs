
namespace ApplicationDefaults.Exceptions
{
    public static class ErrorCodes
    {

        #region Generic 
        public const string DataBaseRecordNotFound = "5656-DATABASERECORDNOTFOUND";
        public const string EnumTypeDoesNotExist = "5656-ENUMTYPEDOESNOTEXIST";
        public const string TypeIsNotSupported = "5656-TYPEISNOTSUPPORTED";
        public const string MissingBody = "5656-MISSINGBODYINTHEREQUEST";
        public const string MissingParametar = "5656-MISSINGPARAMETARINTHEREQUEST";
        public const string MissingValue = "5656-MISSINGVALUEINTHEREQUEST";
        #endregion


        #region Specific
        public const string TradeCantBeExecuted = "5656-TRADECANTBEEXECUTED";
        public const string MaxCenterLimitReached = "5656-MAXCENTERLIMITREACHED";
        public const string TeamMaxPlayersReached = "5656-TEAMMAXPLAYERSREACHED";
        public const string TeamNameAlreadyInLeague = "5656-TEAMNAMEALREADYINLEAGUE";
        public const string LoginFailed = "5656-LOGINFAILED";
        public const string DraftAlreadyStarted = "5656-DRAFTALREADYSTARTED";  

        #endregion

    }
}
