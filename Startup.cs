using Microsoft.AspNetCore.Hosting;
using Serilog;
using worker_socket.emisores;
using worker_socket.services;
using worker_socket.utils;

namespace worker_socket
{
    public class Startup
    {
        private readonly IHostBuilder _hostBuilder;

        public Startup(IHostBuilder hostBuilder)
        {
            this._hostBuilder = hostBuilder;
        }

        public void ConfigureServices()
        {
            this._hostBuilder.ConfigureServices((hostContext, services) =>
            {
                if (hostContext.HostingEnvironment.IsDevelopment())
                {
                    Log.Warning("----------En ambiente de desarrollo");
                }
                else
                {

                }
                ServicesUtil(services);
                ServicesController(services);
                /* ServicesRepository(services);
                ServicesModules(services); */
                Services(services);
            });
        }
        public void ServicesUtil(IServiceCollection services)
        {
            //Add services
            services.AddSingleton<ApiRest>();
            services.AddSingleton<Jwt>();
            
            /* services.AddSingleton<DBRallyDiciembre2023>();
            services.AddTransient<EmailHttpClient>();
            services.AddTransient<EmailRequest>(); */
        }

        public void ServicesController(IServiceCollection services)
        {
            //Add services
            services.AddSingleton<GeneralController>();

        }
        public void Services(IServiceCollection services)
        {
            //Add services
            services.AddHostedService<ClienteSocket>();
        }

        /*Modulos*/
        /*   public void ServicesModules(IServiceCollection services)
          {
              services.AddTransient<ActualizacionRallyModulo>();
              services.AddTransient<ConexionSocketsService>();
          } */

        /*Repositorio*/
        /*  public void ServicesRepository(IServiceCollection services)
         {
             services.AddTransient<MiembrosRepository>();
             services.AddTransient<VwListaVentaGralRepository>();
             services.AddTransient<MiembroAvanceRepository>();
             services.AddTransient<AvanceRepository>();
         } */
    }
}