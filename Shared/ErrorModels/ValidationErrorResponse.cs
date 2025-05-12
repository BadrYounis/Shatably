namespace Shared.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; }
        public string errorMessage { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }
    }
    public class ValidationError
    {
        public string Field { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
