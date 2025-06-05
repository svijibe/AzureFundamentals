using AzureTangyFunc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureTangyFunc
{
    public class OnSalesUploadWriteToQueue
    {
        private readonly ILogger _logger;

        public OnSalesUploadWriteToQueue(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<OnSalesUploadWriteToQueue>();
        }

        [Function("OnSalesUploadWriteToQueue")]
        [QueueOutput("SalesRequestOutBound", Connection = "AzureWebJobsStorage")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SalesRequest? data = JsonConvert.DeserializeObject<SalesRequest>(requestBody);
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            //return data ?? new SalesRequest();
            return new OkObjectResult(data ?? new SalesRequest());

            //var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            //response.WriteString("Welcome to Azure Functions!");

            //return response;
        }
    }
}
