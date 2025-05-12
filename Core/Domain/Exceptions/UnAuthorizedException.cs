namespace Domain.Exceptions;
public sealed class UnAuthorizedException(string msg = "Invalid E-mail or Password") : Exception(msg)
{
}
