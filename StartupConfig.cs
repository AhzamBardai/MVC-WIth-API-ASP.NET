using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using AspFinal.Utils;

namespace AspFinal {
    // this doesnot work with routing constructs between mvc and api
    public static class StartupConfig {
        // Map to /api doesnot recognize /api as a different url so both api and mvc controllers hit the api controller
        public static IApplicationBuilder Api( this IApplicationBuilder app, IWebHostEnvironment env ) {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "webapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallback(c => 
                    c.Response.WriteAsJsonAsync($"Enter Controller route you would like to access. Available Controllers: {ApiUtils.ApiControllers()}"));
            });
            return app;
        }

        // Map config for /mvc route but app.Map does not work with home route(/)
        public static IApplicationBuilder Mvc( this IApplicationBuilder app, IWebHostEnvironment env ) {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            return app;
        }
    }
}