namespace Specification.Lite.Exceptions;

public class SelectorNotFoundException : Exception
{
    private new const string Message = "The specification must have a selector transform defined. Ensure either Select() or SelectMany() is used in the specification.";

    public SelectorNotFoundException()
        : base(Message)
    {
    }

    public SelectorNotFoundException(Exception innerException)
        : base(Message, innerException)
    {
    }
}
