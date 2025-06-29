namespace Specification.Lite.Exceptions;

public class ConcurrentTrackingException : Exception
{
    private new const string Message = "Cannot apply both AsNoTracking and AsTracking on the same specification.";

    public ConcurrentTrackingException()
        : base(Message)
    {
    }

    public ConcurrentTrackingException(Exception innerException)
        : base(Message, innerException)
    {
    }
}