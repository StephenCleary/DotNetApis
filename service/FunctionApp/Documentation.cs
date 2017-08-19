using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Common;
using Logic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Nuget;

namespace FunctionApp
{
    public static class Documentation
    {
        [FunctionName("Documentation")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "0/doc")]HttpRequestMessage req, TraceWriter log)
        {
            Defaults.ApplyRequestHandlingDefaults(req);
            try
            {
                var logger = new Logger(log, "Doc API", Guid.NewGuid());

                var query = req.GetQueryNameValuePairs().ToList();
                var jsonVersion = query.Required("jsonVersion", int.Parse);
                var packageId = query.Required("packageId");
                var packageVersion = query.Optional("packageVersion");
                var targetFramework = query.Optional("targetFramework");
                logger.Trace($"Received request for {jsonVersion}, {packageId} {packageVersion} {targetFramework}.");

                if (jsonVersion < JsonFactory.Version)
                    return req.CreateResponse((HttpStatusCode) 422, "Application needs to update; refresh the page.");

                var handler = new DocRequestHandler(logger, new NugetRepository());
                var result = handler.GetDoc(packageId, packageVersion);

                return req.CreateResponse(HttpStatusCode.OK, "Hello " + result);
            }
            catch (ExpectedException ex)
            {
                return req.CreateErrorResponse(ex);
            }
        }
    }
}
