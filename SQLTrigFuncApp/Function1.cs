using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace SQLTrigFuncApp
{
    public class MultiResponse
    {
        [ServiceBusOutput("ordersqueue", Connection = "QueueConnectionString")]
        public string[] Messages { get; set; }
    }
    public class Function1
    {
        [Function("Function1")]
        public static MultiResponse Run(
            [SqlTrigger("[dbo].[Orders]", "ConnectionStrings")] IReadOnlyList<SqlChange<Orders>> changes,
                FunctionContext context)
        {
            var logger = context.GetLogger("Function1");

            // Prepare messages to send to the Service Bus queue

            var queueMessages = new List<string>();

            foreach (var change in changes)
            {
                logger.LogInformation("Operartion:"+ change.Operation);
                logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(change.Item));
                queueMessages.Add(JsonConvert.SerializeObject(change.Item));

            }
            return new MultiResponse
            {
                Messages = queueMessages.ToArray()
            };
        }
    }

    public class Orders
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string? State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
    }
}
