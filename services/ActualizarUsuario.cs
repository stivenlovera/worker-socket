using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using worker_socket.emisores;

namespace worker_socket.services
{
    public class ActualizarUsuario : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IServiceProvider serviceProvider;

        public ActualizarUsuario(
            ILogger<Worker> logger,
            IServiceProvider serviceProvider
        )
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            using (var scope = this.serviceProvider.CreateAsyncScope())
            {
                scope.ServiceProvider.GetRequiredService<UsuarioSocket>().Escuchando();
            }
            /* while (!stoppingToken.IsCancellationRequested)
            {
                this.logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //await this.usuarioSocket.Escuchando();

                //await request.RequestFormData();
                await Task.Delay(5000, stoppingToken);

            } */
        }
    }
}