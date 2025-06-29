namespace Specification.Lite.Exceptions;

public class ConcurrentSelectorsException : Exception
{
    private new const string Message = "Cannot apply both Select and SelectMany on the same projection specification.";

    public ConcurrentSelectorsException()
        : base(Message)
    {
    }

    public ConcurrentSelectorsException(Exception innerException)
        : base(Message, innerException)
    {
    }
}
