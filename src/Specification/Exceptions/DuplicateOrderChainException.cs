namespace Specification.Lite.Exceptions;

public class DuplicateOrderChainException : Exception
{
    private new const string Message = "The specification contains more than one Order chain.";

    public DuplicateOrderChainException()
        : base(Message)
    {
    }

    public DuplicateOrderChainException(Exception innerException)
        : base(Message, innerException)
    {
    }
}
