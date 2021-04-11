using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceSalesMessages;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.FileSystem;

namespace ServiceSalesApi
{
    public static class RebusExtensions
    {
        public static void AddRebusAsOneWayClient(this IServiceCollection services, IConfiguration config)
        {
            var baseDir = "/home/gontijo/Documentos/Projects/TW/Rebus";

            services.AddRebus(
                rebus => rebus
                   .Logging(l => l.Console())
                   .Routing(r => r.TypeBased()
                                    .Map<ProposalRequest>("MainQueue")
                                    .Map<ApproveProposal>("MainQueue"))
                   .Transport(t => t.UseFileSystem(baseDir, "MainQueue"))
                   .Options(t => t.SimpleRetryStrategy(errorQueueAddress: "ErrorQueue")));
        }
    }
}
