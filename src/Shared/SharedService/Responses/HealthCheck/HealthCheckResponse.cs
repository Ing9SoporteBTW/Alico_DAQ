namespace SharedService.Responses.HealthCheck
{
    using System;
    using System.Collections.Generic;

    public class HealthCheckResponse
    {
        public string status { get; set; }
        public IEnumerable<HealthCheck> Checks { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
