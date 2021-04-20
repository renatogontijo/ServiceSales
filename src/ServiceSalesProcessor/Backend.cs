using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using Rebus.ServiceProvider;

namespace ServiceSalesProcessor
{
    public class Backend : IDisposable
    {
        private readonly ServiceProvider _provider;
        private IBus _bus;

        public Backend(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            services.AddRebusAsSendAndReceive(configuration);
            _provider = services.BuildServiceProvider();
            _provider.UseRebus(x => _bus = x);
        }

        public void Dispose()
        {
            _bus?.Dispose();
            _provider?.Dispose();
        }
    }
}