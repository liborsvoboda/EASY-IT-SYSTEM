using System.Linq;
using EasyITSystemCenter.GlobalOperations;
using System;
using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace EasyITSystemCenter.WebServer {

    //https://learn.microsoft.com/en-us/aspnet/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api

    //Route /SystemApi/SystemConfiguration - Used from Class Name as Controller Name
    public class SystemConfigurationController : ApiController {


        private async Task<string> GetLanguageCodeAsync() {
            try {
                string data = SystemOperations.GetLanguageCode(App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);
                return JsonSerializer.Serialize(data, new JsonSerializerOptions() {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            } catch (Exception ex) { 
                return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = 0, ErrorMessage = await SystemOperations.GetExceptionMessages(ex) });
            }
            
        }


    }
}