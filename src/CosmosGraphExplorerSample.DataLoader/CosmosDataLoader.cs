using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGraphExplorerSample.DataLoader
{
    class CosmosDataLoader
    {
        private readonly CosmosConfig config;

        public CosmosDataLoader(CosmosConfig config)
        {
            this.config = config;
        }

        public void Start()
        {
            var server = GetServer();

            using (var gremlinClient = GetClient(server))
            {

            }
        }

        private GremlinClient GetClient(GremlinServer server)
        {
            return new GremlinClient(server,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType);
        }

        private GremlinServer GetServer()
        {
            var username = $"/dbs/{config.DatabaseName}/colls/{config.GraphName}";

            var gremlinServer = new GremlinServer(config.Endpoint, config.Port, enableSsl: true, 
                username: username, password: config.AuthKey);

            return gremlinServer;
        }
    }
}
