using System.Text.Json;

namespace Shared.ErrorModels
{
    public class ErrorDetails //Response Returned
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string>? errors { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
