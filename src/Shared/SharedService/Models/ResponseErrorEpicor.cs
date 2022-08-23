namespace SharedService.Models
{
    public class ResponseErrorEpicor
    {
        public string HttpStatus { get; set; }
        public string ReasonPhrase { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorType { get; set; }
        public object ErrorDetails { get; set; }
        public string CorrelationId { get; set; }
    }
}
