using System.Net;

namespace EcoTracker.Core.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomResponseCode : Attribute
    {
        public HttpStatusCode ResponseCode { get; }

        public CustomResponseCode(HttpStatusCode responseCode)
        {
            ResponseCode = responseCode;
        }
    }
}
