namespace Shared
{
    public class OperationResult<T>
    {
        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }

        public static OperationResult<T> Ok(T data)
            => new OperationResult<T> { Succeeded = true, Data = data };

        public static OperationResult<T> Fail(string errorMessage)
            => new OperationResult<T> { Succeeded = false, ErrorMessage = errorMessage };
    }
}
