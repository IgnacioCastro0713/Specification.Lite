namespace Specification.Lite.Exceptions;

public class DuplicateTakeException : Exception
{
    private new const string Message = "Duplicate use of Take(). Ensure you don't use Take() more than once in the same specification!";

    public DuplicateTakeException()
        : base(Message)
    {
    }

    public DuplicateTakeException(Exception innerException)
        : base(Message, innerException)
    {
    }
}
