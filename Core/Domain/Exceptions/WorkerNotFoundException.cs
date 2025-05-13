namespace Domain.Exceptions;
public sealed class WorkerNotFoundException : NotFoundException
{
    public WorkerNotFoundException(Guid id) : base($"Worker With Id: {id} Not Found")
    {

    }
}
