namespace Specification.Lite.Exceptions;

public class DuplicateSelectorsException : Exception
{
    private new const string Message = "A selector (Select or SelectMany) must be applied to a ProjectionSpecification.";

    public DuplicateSelectorsException()
        : base(Message)
    {
    }

    public DuplicateSelectorsException(Exception innerException)
        : base(Message, innerException)
    {
    }
}
