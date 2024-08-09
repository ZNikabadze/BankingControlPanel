namespace BankingControlPanel.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainErrorCodes ErrorCode { get; set; }
        public DomainException(DomainErrorCodes code) : base(code.ToString())
        {
            ErrorCode = code;
        }
    }
}
