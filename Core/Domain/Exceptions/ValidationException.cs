namespace Domain.Exceptions;
public class ValidationException : Exception
{
    public List<string> Errors { get; }
    public ValidationException(IEnumerable<string> errors)
        : base("Validation failed: " + string.Join("; ", errors))
    {
        Errors = errors.ToList();
    }
}
