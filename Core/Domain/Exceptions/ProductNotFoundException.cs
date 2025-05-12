namespace Domain.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base($"Product With Id: {id} Not Found")
        {

        }
    }
}
