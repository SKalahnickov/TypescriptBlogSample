using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TypescriptApp.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddMvc(act => act.EnableEndpointRouting = false);
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "Home/Index";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();
			app.UseMvc();
			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = env.ContentRootPath;

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});

			app
				.UseMvc()
				.MapWhen(
				context => !context.Request.Path.Value.StartsWith("/api"),
				builder => builder
					.UseMvc(routes =>
					{
						routes.MapSpaFallbackRoute(
						name: "spa-fallback",
						defaults: new { controller = "Home", action = "Index" });
					})
			);
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
