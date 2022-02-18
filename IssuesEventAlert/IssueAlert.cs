using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IssuesEventAlert.Service;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace IssuesEventAlert
{
    public class IssueAlert
    {
        private readonly IIssueAlertService _service;
        public IssueAlert( IIssueAlertService service )
        {
            _service = service;
        }

        [FunctionName("IssueAlerts")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "Jira",
            collectionName: "Issues",
            ConnectionStringSetting = "CosmoDBString",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {
            await _service.NotifyEvent(input[0].Id, log);
        }
    }
}
