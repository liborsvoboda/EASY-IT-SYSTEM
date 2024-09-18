using EASYDATACenter.DBModel;

namespace EASYDATACenter.Controllers {
    [Authorize]
    [ApiController]
    [Route("GLOBALNETTemplateAnySProcedureData")]
    public class GLOBALNETTemplateAnySProcedureDataListApi : ControllerBase {
    
        [HttpGet("/GLOBALNETTemplateAnySProcedureData")]
        public async Task<string> GetTemplateAnySProcedureDataList() {
            List<CustomString> data = new();
            data = new EASYDATACenterContext().CollectionFromSql<CustomString>("EXEC GetTables;");

            return JsonSerializer.Serialize(data, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true });
        }
    }
    
    
        /// <summary>
        /// Gets Form Agendas Pages List
        /// For System Menu Definition.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/GoldenSystemStoredProceduresList/SystemSpGetSystemPageList")]
        public async Task<string> GetSystemTableList() {
            try {
                List<CustomString> data = new();
                data = new GoldenContext().GoldenCollectionFromSql<CustomString>("EXEC SystemSpGetSystemPageList;");
                return JsonSerializer.Serialize(data, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true });
            } catch (Exception ex) {
                return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = 0, ErrorMessage = ServerCoreHelpers.GetUserApiErrMessage(ex) });
            }
        }
    
        /// <summary>
        /// Api For Logged User 
        /// with Menu Datalist
        /// </summary>
        /// <returns></returns>
        [HttpGet("/GoldenSystemStoredProceduresList/SystemSpGetUserMenuList")]
        public async Task<string> GetSystemSpGetUserMenuList() {
            try {
                List<SpUserMenuList> data = new List<SpUserMenuList>();

                data = new GoldenContext().GoldenCollectionFromSql<SpUserMenuList>("EXEC SystemSpGetUserMenuList @userRole = N'" + User.FindFirst(ClaimTypes.Role.ToString())?.Value + "';");
                return JsonSerializer.Serialize(data, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true });
            } catch (Exception ex) {
                return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = 0, ErrorMessage = ServerCoreHelpers.GetUserApiErrMessage(ex) });
            }
        }
}