namespace Lathorva.Common.Web
{
    /// <summary>
    /// Custom conflict error, e.g username already exists
    /// </summary>
    public class ConflictError
    {
        public ConflictError(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; }
        public string Message { get; }
    }
}
