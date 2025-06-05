using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using AzureTangyFunc.Models;
using Newtonsoft.Json;


public class Function1
{
    private readonly ILogger<Function1> _logger;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
    }

    [Function("Function1")]
    [QueueOutput("SalesRequestOutBound", Connection = "AzureWebJobsStorage")]
    public async Task<SalesRequest> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        SalesRequest? data = JsonConvert.DeserializeObject<SalesRequest>(requestBody);
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return data ?? new SalesRequest();

    }
}