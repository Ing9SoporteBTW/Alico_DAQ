namespace SharedService.Responses
{
    public static class Shared
    {
        public static PetitionResponse Answer(bool _success, string _message, string _module, string _url, object _result)
        {
            return new PetitionResponse()
            {
                success = _success,
                message = _message,
                module = _module,
                URL = _url,
                result = _result
            };
        }
    }
}
