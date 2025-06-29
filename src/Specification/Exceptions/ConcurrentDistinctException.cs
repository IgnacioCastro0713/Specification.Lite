namespace Specification.Lite.Exceptions;

public class ConcurrentDistinctException : Exception
{
    private new const string Message = "Cannot apply both DistinctBy and Distinct on the same specification. Use only one.";


    public ConcurrentDistinctException()
        : base(Message)
    {
    }

    public ConcurrentDistinctException(Exception innerException)
        : base(Message, innerException)
    {
    }
}
