namespace Domain.Contracts
{
    public interface IDbInitializer
    {
        Task InitializeIdentityAsync();
    }
}
