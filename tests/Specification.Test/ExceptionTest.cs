using Specification.Lite.Exceptions;

namespace Specification.Test;

public class ExceptionTests
{
    [Fact]
    public void SelectorNotFoundException_ShouldHaveCorrectMessage()
    {
        var exception = new SelectorNotFoundException();
        Assert.Equal("The specification must have a selector transform defined. Ensure either Select() or SelectMany() is used in the specification.", exception.Message);
    }

    [Fact]
    public void SelectorNotFoundException_ShouldPropagateInnerException()
    {
        var innerException = new SelectorNotFoundException();
        var exception = new SelectorNotFoundException(innerException);

        Assert.Equal("The specification must have a selector transform defined. Ensure either Select() or SelectMany() is used in the specification.", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void DuplicateTakeException_ShouldHaveCorrectMessage()
    {
        var exception = new DuplicateTakeException();
        Assert.Equal("Duplicate use of Take(). Ensure you don't use Take() more than once in the same specification!", exception.Message);
    }

    [Fact]
    public void DuplicateTakeException_ShouldPropagateInnerException()
    {
        var innerException = new SelectorNotFoundException();
        var exception = new DuplicateTakeException(innerException);

        Assert.Equal("Duplicate use of Take(). Ensure you don't use Take() more than once in the same specification!", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void DuplicateSkipException_ShouldHaveCorrectMessage()
    {
        var exception = new DuplicateSkipException();
        Assert.Equal("Duplicate use of Skip(). Ensure you don't use Skip() more than once in the same specification.", exception.Message);
    }

    [Fact]
    public void DuplicateSkipException_ShouldPropagateInnerException()
    {
        var innerException = new SelectorNotFoundException();
        var exception = new DuplicateSkipException(innerException);

        Assert.Equal("Duplicate use of Skip(). Ensure you don't use Skip() more than once in the same specification.", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }
}
