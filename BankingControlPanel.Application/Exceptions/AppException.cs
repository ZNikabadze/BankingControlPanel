namespace BankingControlPanel.Application.Exceptions
{
    public class AppException : Exception
    {
        public AppErrorCodes ErrorCode { get; set; }
        public AppException(AppErrorCodes code) : base(code.ToString())
        {
            ErrorCode = code;
        }
    }
}
