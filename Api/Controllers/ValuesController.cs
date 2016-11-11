using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Http;

namespace Api.Controllers
{
    public class ValuesController : ApiController
    {
        static int contador = 0;

        static CloudQueue cloudQueue;

        static ValuesController()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=duelkings;AccountKey=jC3xf7IPO7sXZs334WeOQ7BVn7Ro+9GZObpvXTcXUafidAHqiA8blEr8H8jj5hPUcH1v4zb+mqbBnDZ7mwtdWw==";

            CloudStorageAccount cloudStorageAccount;

            if (!CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {
                Trace.TraceInformation("Deu erro");
            }

            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            cloudQueue = cloudQueueClient.GetQueueReference("demoqueue");

            // Note: Usually this statement can be executed once during application startup or maybe even never in the application.
            //       A queue in Azure Storage is often considered a persistent item which exists over a long time.
            //       Every time .CreateIfNotExists() is executed a storage transaction and a bit of latency for the call occurs.
            cloudQueue.CreateIfNotExists();
        }

        // GET api/values
        public string Get()
        {
            return "Samuel Chamon" + (++contador);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(Pedido value)
        {
            var message = new CloudQueueMessage(JsonConvert.SerializeObject(value));

            cloudQueue.AddMessage(message);
        }
      

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
