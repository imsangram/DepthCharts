
namespace DepthCharts.Core.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException(string entityName, object value)
        : base($"{entityName} already exists.")
    {
    }

    public EntityAlreadyExistsException(string entityName, Exception innerException)
        : base($"{entityName} already exists.", innerException)
    {
    }
}

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, object key)
        : base($"{entityName} with key {key} was not found.")
    {
    }

    public EntityNotFoundException(string entityName, object key, Exception innerException)
        : base($"{entityName} with key {key} was not found.", innerException)
    {
    }
}
