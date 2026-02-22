namespace ApplicationDefaults.Exceptions
{
    public class NBAException : Exception
    {
        public int ErrorCode { get; }
        public NBAException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        public NBAException(string message, int errorCode, Exception exceptionInner) : base(message, exceptionInner)
        {
            ErrorCode = errorCode;
        }
    }
}
