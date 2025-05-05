using System.IO;
using System;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Extensions;
using EasyITSystemCenter.GlobalClasses;
using System.Linq;
using Microsoft.Owin.StaticFiles.ContentTypes;
using System.Web.Http;
using EasyITSystemCenter.GlobalOperations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyITSystemCenter {

    /// <summary>
    /// Local WebServwer Startup
    /// </summary>
    public class Startup {

        public void Configuration(IAppBuilder appBuilder) {

            //STATIC APP HOSTING AND WEB BROWSER 
            var staticFilesProvider = new FileExtensionContentTypeProvider();
            staticFilesProvider.Mappings[".javascript"] = "application/javascript"; staticFilesProvider.Mappings[".style"] = "text/css";
            staticFilesProvider.Mappings[".json"] = "text/json"; staticFilesProvider.Mappings[".code"] = "text/cs";
            staticFilesProvider.Mappings[".xaml"] = "text/xaml"; staticFilesProvider.Mappings[".archive"] = "application/zip";
            staticFilesProvider.Mappings[".markdown"] = "text/markdown"; staticFilesProvider.Mappings[".md"] = "text/markdown";

            StaticFileOptions staticStorage = new StaticFileOptions {
                ServeUnknownFileTypes = true, ContentTypeProvider = staticFilesProvider, DefaultContentType = "text/html",
                FileSystem = new PhysicalFileSystem(App.appRuntimeData.webDataPath), RequestPath = new PathString(string.Empty)
            };
            bool browseModeEnabled = bool.Parse(App.appRuntimeData.AppClientSettings.FirstOrDefault(a => a.Key == "sys_localWebServerEnableBrowse").Value);
            FileServerOptions fileServer = new FileServerOptions {
                EnableDefaultFiles = true,
                FileSystem = new PhysicalFileSystem(App.appRuntimeData.webDataPath),
                EnableDirectoryBrowsing = browseModeEnabled,
                RequestPath = new PathString(string.Empty),
                DefaultFilesOptions = { FileSystem = new PhysicalFileSystem(App.appRuntimeData.webDataPath),
                    DefaultFileNames = { "index.html", "index.md", "help.html", "help.md", "example.html", "example.md" },
                RequestPath = new PathString(string.Empty)}, DirectoryBrowserOptions = {
                FileSystem =new PhysicalFileSystem(App.appRuntimeData.webDataPath),
                RequestPath=new PathString(string.Empty)
                }
            };
            appBuilder.UseStaticFiles(staticStorage).UseFileServer(fileServer);
            if (browseModeEnabled) { appBuilder.UseDirectoryBrowser(); }
            appBuilder.UseStageMarker(PipelineStage.MapHandler);




            //var cfg = new HttpConfiguration(); cfg.MapHttpAttributeRoutes();
            //cfg.Routes.MapHttpRoute(name: "OtherApi", routeTemplate: "SystemApi", defaults: new { controller = "TestApi", action = "Test" });
            //cfg.Routes.MapHttpRoute(name: "SystemApi", routeTemplate: "SystemApi/{controller}/{action}/{id}", defaults: new { id = RouteParameter.Optional });
            //appBuilder.UseWebApi(cfg);
           



            //ESB LOCAL hosting API SERVICES
            appBuilder.Use(async (context, next) =>
            {
                var appData = App.appRuntimeData;
            if (context.Request.Path.ToString() == "/SystemApi/GetSystemLanguage") {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(
                    SystemOperations.GetLanguageCode(App.appRuntimeData.AppClientSettings.FirstOrDefault(a => a.Key == "sys_defaultLanguage").Value), 
                    new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
                return;
            }
                





                await next();
            });
        }
    }
}
