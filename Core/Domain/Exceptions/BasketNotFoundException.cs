namespace Domain.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string id) : base($"The Basket With ID : {id} IS NOT FOUND !! ") { }
    }
}
