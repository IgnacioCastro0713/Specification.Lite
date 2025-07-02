namespace Specification.Lite.Exceptions;

public class DuplicateSkipException : Exception
{
    private new const string Message = "Duplicate use of Skip(). Ensure you don't use Skip() more than once in the same specification.";

    public DuplicateSkipException()
        : base(Message)
    {
    }

    public DuplicateSkipException(Exception innerException)
        : base(Message, innerException)
    {
    }
}
