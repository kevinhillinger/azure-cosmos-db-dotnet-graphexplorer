using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGraphExplorerSample.DataLoader
{
    class CosmosConfig
    {
        /// <summary>
        /// The gremlin endpoint that's provided by Cosmos DB
        /// </summary>
        public string Endpoint { get; set; }

        public int Port { get; } = 443;

        /// <summary>
        /// The authentication key used to connect to Cosmos DB
        /// </summary>
        public string AuthKey { get; set; }

        /// <summary>
        /// The name of the database
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The name of the graph (also called the "collection")
        /// </summary>
        public string GraphName { get; set; }

    }
}
