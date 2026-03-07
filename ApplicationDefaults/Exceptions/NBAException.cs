namespace ApplicationDefaults.Exceptions
{
    public class NBAException : Exception
    {
        public string ErrorCode { get; }
        public NBAException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        public NBAException(string message, string errorCode, Exception exceptionInner) : base(message, exceptionInner)
        {
            ErrorCode = errorCode;
        }
    }
}
