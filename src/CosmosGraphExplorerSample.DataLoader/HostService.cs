using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CosmosGraphExplorerSample.DataLoader
{
    /// <summary>
    /// This is just plumbing to start the data loader
    /// </summary>
    class HostService : IHostedService
    {
        private readonly CosmosDataLoader dataLoader;

        public HostService(CosmosDataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            dataLoader.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
