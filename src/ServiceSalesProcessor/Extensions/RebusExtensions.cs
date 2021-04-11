using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceSalesMessages;
using Rebus.Auditing.Messages;
using Rebus.Config;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.FileSystem;
using Rebus.Persistence.FileSystem;

namespace ServiceSalesProcessor
{
    public static class RebusExtensions
    {
        public static void AddRebusAsSendAndReceive(this IServiceCollection services, IConfiguration config)
        {
            var baseDir = "/home/gontijo/Documentos/Projects/TW/Rebus";

            services.AddRebus(
                rebus => rebus
                   .Logging  (l => l.Serilog())
                   .Routing  (r => r.TypeBased().MapAssemblyOf<ProposalRequest>("MainQueue"))
                   .Transport(t => t.UseFileSystem(baseDir, "MainQueue"))
                   .Options  (t => t.SimpleRetryStrategy(errorQueueAddress: "ErrorQueue"))
                   .Sagas    (s => s.UseFilesystem($"{baseDir}/Sagas"))
                );

            services.AutoRegisterHandlersFromAssemblyOf<Backend>();
        }
    }
}