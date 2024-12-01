namespace LilaRent.Application.Exceptions;


public class EntityNotFoundException : Exception
{
    public object NonExistentIdentifier { get; init; }


    public EntityNotFoundException(object nonExistentId)
        : base()
    {
        NonExistentIdentifier = nonExistentId;
    }

    public EntityNotFoundException(object nonExistentId, string message)
        : base(message)
    {
        NonExistentIdentifier = nonExistentId;
    }

    public EntityNotFoundException(object nonExistentId, string message, Exception innerException)
        : base(message, innerException)
    {
        NonExistentIdentifier = nonExistentId;
    }
}
