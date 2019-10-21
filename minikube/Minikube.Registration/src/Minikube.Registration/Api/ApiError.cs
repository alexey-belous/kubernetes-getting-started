using System;

namespace Minikube.Registration.Api
{
    public class ApiError
    {
        public Guid ApiInstanceId { get; } = Startup.InstanceId;
        public DateTimeOffset Timestamp { get; }
        public string ErrorMessage { get; }

        public ApiError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}