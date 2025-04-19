using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace EasyITSystemCenter.GlobalClasses {

    /// <summary>
    /// !!!SYSTEM Global Runtime Monitor Definition For One Point monitoring For Processes and each
    /// other Definition For optimize the System Running
    ///
    /// TODO
    /// - move All Central Definitions Here
    /// - create Monitor Window for managing
    /// </summary>
    public class AppRuntimeData {
        private static readonly Version version = Assembly.GetExecutingAssembly().GetName().Version;
        private static readonly string appSystemStatuses = Debugger.IsAttached ? SystemStatuses.Debug.ToString() : SystemStatuses.Release.ToString();

        public string startupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string programDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        public string settingFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Assembly.GetEntryAssembly().GetName().FullName.Split(',')[0]);
        public string reportFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Assembly.GetEntryAssembly().GetName().FullName.Split(',')[0], "Reports");
        public string updateFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Assembly.GetEntryAssembly().GetName().FullName.Split(',')[0], "Update");
        public string tempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Assembly.GetEntryAssembly().GetName().FullName.Split(',')[0], "Temp");
        public string galleryFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Assembly.GetEntryAssembly().GetName().FullName.Split(',')[0], "Gallery");
        public string appDataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"Data", "AddOn", "AppData");
        public string webDataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data", "AddOn", "WebData");
        public string webSrvDataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data", "AddOn", "WebSrvData");
        public string webDataUrlPath = Path.Combine("Data", "AddOn", "WebData"); //For OWIN
        public string appSettingFile = "config.json";
        public bool webServerRunning = false;


        public string appName = Assembly.GetEntryAssembly().GetName().FullName.Split(',')[0];
        public Version AppVersion { get => version; }
        public string AppSystemStatuses { get => appSystemStatuses; }
        public string appStartupLanguage = Thread.CurrentThread.CurrentCulture.ToString();

        public Dictionary<string, string> AppClientSettings = new Dictionary<string, string>();

        public CoreWebView2Environment WebViewEnvironment = CoreWebView2Environment.CreateAsync(null,
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Assembly.GetEntryAssembly().GetName().FullName.Split(',')[0], "WebView"),
            new CoreWebView2EnvironmentOptions()).GetAwaiter().GetResult();

        /// <summary>
        /// JSON Predefined
        /// </summary>
        public JsonSerializerOptions JsonSerializeOptions = new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true, ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

    }

    /// <summary>
    /// !!SYSTEM Global Definition for System Statuses
    ///
    /// SYSTEM Running mode In debug mode is disabled the System Logger Visual Studio Debugger
    /// difficult operation has problem If you want you can enable SystemLogger by change to: DebugWithSystemLogger
    ///
    /// Its Used as String EveryWhere Its good Soution for Centarized Statuses of System Errors Are
    /// Saved In SystemLogger or Database
    /// </summary>
    public enum SystemStatuses {

        //Mode
        Debug,
        Release,
        DebugWithSystemLogger
    }


    /// <summary>
    /// Defined Url Sourvce types For Setting Corrtect UrlPath
    /// With Requested Configurations: Auth, Message Format, etc.
    /// Definitions:
    /// EsbWebServer: AutoSelect ESB GloBal Web Server Url 
    /// EicWebServer: AutoSelect EIC GloBal Web Server Url 
    /// EicWebServerStdTableApi: Auto Select Api By Data Table Name
    /// EicWebServerDynGetTableApi: Auto Select DynamicGetApi Url
    /// EicWebServerDynSetTableApi: Auto Select DynamicSetApi Url
    /// WebSavedAuthApi: Managed Acount for User Defined Url,
    /// WebUrl: User Defined Url
    /// TODO Pridat centralni sprava auth APIs URL, 
    /// TODO Planovany Downloader, zasilac zprac cteni a preposilani na email,Procedury Atd
    /// TODO: FOR TABLES ADD - MORE PREFIXES - TEST , DB TEST, CLONE TABLE, CLONE DB as PROVIDER solution 
    /// TODO: MOVE THIS TO NEW AGENDA DEFINED API COMMUNICATION + Same FOR Request,responose format forGlobal Using
    /// TODO: Can be used for showing JSON DATA, XML, CSV, DATA FOR DIRECT IMPORT BY DYNAMIC AGENDA - CAN GENERATE TABLE BY TABLE STRUCTURE
    /// </summary>
    public enum UrlSourceTypes {

        //Mode
        EsbWebServer,
        EicWebServer,
        EicWebServerAuth,
        EicWebServerStdTableApi,
        EicWebServerGenericGetTableApi,
        EicWebServerGenericSetTableApi,
        WebApiManagerApi,
        WebUrl,
        JsonDataUrl
    }


    /// <summary>
    /// Database response types definition
    /// </summary>
    public enum DBResult {
        success,
        error,
        DeniedYouAreNotAdmin,
        UnauthorizedRequest
    }

    /// <summary>
    /// Class for User Authentication information
    /// </summary>
    public class AuthentificationResponse {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Token { get; set; } = null;
        public string Email { get; set; } = null;
        public string Message { get; set; } = null;
        public DateTime? Expiration { get; set; } = null;
        public string Role { get; set; }
    }

    /// <summary>
    /// Basic user data for login
    /// </summary>
    public class UserData {
        public string UserName { get; set; }
        public string Password { get; set; }
        public AuthentificationResponse Authentification { get; set; }
    }

    //public class Parameter {
    //    public string Value { get; set; } = string.Empty;
    //    public bool Correct { get; set; } = false;
    //}

    /// <summary>
    /// Tilt Document Types Definitions
    /// </summary>
    public enum TiltTargets {
        None,
        InvoiceToCredit,
        InvoiceToReceipt,
        OfferToOrder,
        OrderToInvoice,
        OfferToInvoice,

        ShowCredit,
        ShowReceipt
    }

    /// <summary>
    /// Univesal Document List (Item) for Offer,Order,Invoice
    /// </summary>
    public partial class DocumentItemList {
        public int Id { get; set; } = 0;
        public string DocumentNumber { get; set; } = null;

        public string PartNumber { get; set; } = null;
        public string Name { get; set; } = null;
        public string Unit { get; set; } = null;
        public decimal PcsPrice { get; set; } = 0;
        public decimal Count { get; set; } = 1;
        public decimal TotalPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalPriceWithVat { get; set; }

        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    /// <summary>
    /// Program version Class
    /// </summary>
    public class AppVersion {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Private { get; set; }
    }

    /// <summary>
    /// Actual List Item informations for Controls each Page in MainView
    /// </summary>
    public class DataViewSupport {
        public int SelectedRecordId { get; set; } = 0;
        public bool FormShown { get; set; } = false;
        public string FilteredValue { get; set; } = null;
        public string AdvancedFilter { get; set; } = null;
    }


    /// <summary>
    /// Generic Inherited Set
    /// </summary>
    public class TranslationSet {
        public string Name { get; set; }
        public string Translation { get; set; }
    }


    /// <summary>
    /// Language definition support
    /// </summary>
    public class TranslateSet {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// Class for Using as customized list the List of API urls for Central using in the system One
    /// Api is One: Dataview / Right / Report Posibility / Menu Item / Page Exist rules for
    /// automatic processing in System Core Logic for simple Developing
    /// </summary>
    public partial class SystemTranslatedTableList {
        public string TableName { get; set; }
        public string Translate { get; set; }
    }

    public interface IGenericRepository<T> where T : class { };
    public class GenericRepository<T> : IGenericRepository<T> where T : class { };

    /// <summary>
    /// Generic Table Standard Fieds Public Class For Get Informations By System
    /// </summary>
    public class GenericTable {
        public int Id { get; set; }
        public string Description { get; set; } = null;
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    /// <summary>
    /// Generická Tabulka Používaná DB Procedurami
    /// je to Objekt ve stringu, nebo string, Cokoliv,
    /// Jen to musí být v tabulce se sloupcem Data
    /// </summary>
    public class GenericDataList {
        public string Data { get; set; }
    }

    /// <summary>
    /// Custom Definition for Returning List with One Record from Operation Stored Procedures
    /// Returrned the "Value"  as Key, Value 
    /// </summary>
    public class GenericValue {
        public string Value { get; set; }
    }

    /// <summary>
    /// Custom Definition for Returning List with One Record from Operation Stored Procedures
    /// </summary>
    public class CustomMessageList {
        public string MessageList { get; set; }
    }

    /// <summary>
    /// Custom Definition for Returning List with One Record from Operation Stored Procedures
    /// </summary>
    public class DeserializedJson {
        public string Key { get; set; }
        public string Value { get; set; }
    }


}