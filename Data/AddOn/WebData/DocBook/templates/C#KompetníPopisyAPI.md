using EASYDATACenter.DBModel;

namespace EASYDATACenter.Controllers {
    /// <summary>
    /// System Template of Authentication Basic / Bearer security Api communication
    /// </summary>
    [ApiController]
    [Route("TemplateAuthApi")]
    public class TemplateAuthApiService : ControllerBase {
        private static Encoding ISO_8859_1_ENCODING = Encoding.GetEncoding("ISO-8859-1");

        /// <summary>
        /// Server Basic Authentication API for login Via UserName and Password Returned is Token
        /// for next communication Token Can be limited by Time - Validating is via LifetimeValidatora
        /// </summary>
        /// <param name="Authorization"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("/TemplateAuthApi")]
        public IActionResult Authenticate([FromHeader] string Authorization) {
            (string username, string password) = GetUsernameAndPasswordFromAuthorizeHeader(Authorization);

            var user = Authenticate(username, password);

            try {
                if (HttpContext.Connection.RemoteIpAddress != null && user != null) {
                    string clientIPAddr = System.Net.Dns.GetHostEntry(HttpContext.Connection.RemoteIpAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
                    if (!string.IsNullOrWhiteSpace(clientIPAddr)) { DBOperations.WriteVisit(clientIPAddr, user.Id, username); }
                }
            } catch { }

            if (user == null)
                return BadRequest(new { message = DBOperations.DBTranslate("UsernameOrPasswordIncorrect", BackendServer.ServerConfigSettings.ConfigServerLanguage) });

            if (!BackendServer.ServerConfigSettings.ServerTimeTokenValidationEnabled) { user.Expiration = null; }

            RefreshUserToken(username, user);
            return Ok(JsonSerializer.Serialize(user));
        }

        private static (string?, string?) GetUsernameAndPasswordFromAuthorizeHeader(string authorizeHeader) {
            if (authorizeHeader == null || (!authorizeHeader.Contains("Basic ") && !authorizeHeader.Contains("Bearer "))) return (null, null);

            if (authorizeHeader.Contains("Basic ")) {
                string encodedUsernamePassword = authorizeHeader.Substring("Basic ".Length).Trim();
                string usernamePassword = ISO_8859_1_ENCODING.GetString(Convert.FromBase64String(encodedUsernamePassword));

                string username = usernamePassword.Split(':')[0];
                string password = usernamePassword.Split(':')[1];

                return (username, password);
            }

            return (null, null);
        }

        /// <summary>
        /// API Authenticated and Generate Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static AuthenticateResponse? Authenticate(string? username, string? password) {
            if (username == null)
                return null;

            var user = new EASYDATACenterContext()
                .UserLists.Include(a => a.Role).Where(a => a.Active == true && a.UserName == username && a.Password == password)
                .First();

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(BackendServer.ServerConfigSettings.ConfigJwtLocalKey);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.SystemName),
                    new Claim(ClaimTypes.Dns, BackendServer.ServerConfigSettings.ConfigCertificateDomain),
                }),
                Issuer = user.UserName,
                NotBefore = DateTimeOffset.Now.DateTime,
                Expires = DateTimeOffset.Now.AddMinutes(BackendServer.ServerConfigSettings.ConfigApiTokenTimeoutMin).DateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            AuthenticateResponse authResponse = new() { Id = user.Id, Name = user.Name, Surname = user.SurName, Token = tokenHandler.WriteToken(token), Expiration = token.ValidTo.ToLocalTime(), Role = user.Role.SystemName };
            return authResponse;
        }

        /// <summary>
        /// API Refresh User Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="token">   </param>
        /// <returns></returns>
        public static bool RefreshUserToken(string username, AuthenticateResponse token) {
            try {
                var dbUser = new EASYDATACenterContext()
                    .UserLists.Where(a => a.Active == true && a.UserName == username)
                    .First();
                if (dbUser == null || dbUser.Token == token.Token && dbUser.Expiration < DateTimeOffset.Now) return false;

                dbUser.Token = token.Token;
                dbUser.Expiration = token.Expiration;
                var data = new EASYDATACenterContext().UserLists.Update(dbUser);
                int result = data.Context.SaveChanges();

                if (result > 0) return true;
                return false;
            } catch (Exception ex) { }
            return false;
        }

        /// <summary>
        /// API Token LifeTime Validator
        /// </summary>
        /// <param name="notBefore"></param>
        /// <param name="expires">  </param>
        /// <param name="token">    </param>
        /// <param name="params">   </param>
        /// <returns></returns>
        internal static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params) {
            if (RefreshUserToken(token.Issuer, new AuthenticateResponse() { Token = ((JwtSecurityToken)token).RawData.ToString(), Expiration = DateTimeOffset.Now.AddMinutes(BackendServer.ServerConfigSettings.ConfigApiTokenTimeoutMin).DateTime }))
                return true;
            else return false;
        }
    }
}
------------------------------------------------------------------------------------------------------------
using EASYDATACenter.DBModel;

namespace EASYDATACenter.Controllers {
    /// <summary>
    /// Universal Template For Make Any Full Backend Server One Template Has All data operation
    /// Controls for simple copy and build ANY Backend Server
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("TemplateList")]
    [SwaggerTag("API Template with INSERT/UPDATE/DELETE/SELECT AND FILTERING APIs")]
    public class TemplateListApi : ControllerBase {
        /// <summary>
        /// Operation: Select All records Standard API for return all records from DB table
        /// </summary>
        /// <returns></returns>
        [HttpGet("/TemplateList")]
        [SwaggerOperation(Summary = "Get All records", Description = "Async standard select record API", OperationId = "Select all records", Tags = new[] { "TemplateListApi" })]
        public async Task<string> GetTemplateList() {
            List<TemplateList> data;
            using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions {
                IsolationLevel = IsolationLevel.ReadUncommitted //with NO LOCK
            })) {
                data = new EASYDATACenterContext().TemplateLists.ToList();
            }

            return JsonSerializer.Serialize(data);
        }

        /// <summary>
        /// Operation: Select By sent SQL Where Condition Standard API for return records by Where
        /// condition in Query from DB table
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("/TemplateList/Filter/{filter}")]
        [SwaggerOperation(Summary = "Get records by Advanced filter", Description = "Async standard select records by advanced filter API", OperationId = "Select records by Advanced filter", Tags = new[] { "TemplateListApi" })]
        public async Task<string> GetTemplateListByFilter(string filter) {
            List<TemplateList> data;
            using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions {
                IsolationLevel = IsolationLevel.ReadUncommitted //with NO LOCK
            })) {
                data = new EASYDATACenterContext().TemplateLists.FromSqlRaw("SELECT * FROM TemplateList WHERE 1=1 AND " + filter.Replace("+", " ")).AsNoTracking().ToList();
            }

            return JsonSerializer.Serialize(data);
        }

        /// <summary>
        /// Operation: Select Unique record Standard API for return one record by primary Id key
        /// from DB table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/TemplateList/{id}")]
        [SwaggerOperation(Summary = "Get Record by Id", Description = "Async standard Get record by Id API", OperationId = "Get One record", Tags = new[] { "TemplateListApi" })]
        public async Task<string> GetTemplateListKey(int id) {
            TemplateList data;
            using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions {
                IsolationLevel = IsolationLevel.ReadUncommitted
            })) {
                data = new EASYDATACenterContext().TemplateLists.Where(a => a.Id == id).First();
            }

            return JsonSerializer.Serialize(data);
        }

        /// <summary>
        /// Operation: Insert new record Standard API for insert new record to DB table
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpPut("/TemplateList")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Create a new Record", Description = "Async standard Insert record API", OperationId = "Insert New Record", Tags = new[] { "TemplateListApi" })]
        public async Task<string> InsertTemplateList([FromBody] TemplateList record) {
            try {
                record.User = null;  //EntityState.Detached IDENTITY_INSERT is set to OFF
                var data = new EASYDATACenterContext().TemplateLists.Add(record);
                int result = await data.Context.SaveChangesAsync();
                if (result > 0) return JsonSerializer.Serialize(new DBResultMessage() { InsertedId = record.Id, Status = DBResult.success.ToString(), RecordCount = result, ErrorMessage = string.Empty });
                else return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = result, ErrorMessage = string.Empty });
            } catch (Exception ex) {
                return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = 0, ErrorMessage = ServerCoreFunctions.GetUserApiErrMessage(ex) });
            }
        }

        /// <summary>
        /// Operation: Update record by unique Id key Standard API for update existing record in DB table
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpPost("/TemplateList")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update Record", Description = "Async standard Update record API", OperationId = "Update Record", Tags = new[] { "TemplateListApi" })]
        public async Task<string> UpdateTemplateList([FromBody] TemplateList record) {
            try {
                var data = new EASYDATACenterContext().TemplateLists.Update(record);
                int result = await data.Context.SaveChangesAsync();
                if (result > 0) return JsonSerializer.Serialize(new DBResultMessage() { InsertedId = record.Id, Status = DBResult.success.ToString(), RecordCount = result, ErrorMessage = string.Empty });
                else return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = result, ErrorMessage = string.Empty });
            } catch (Exception ex) { return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = 0, ErrorMessage = ServerCoreFunctions.GetUserApiErrMessage(ex) }); }
        }

        /// <summary>
        /// Operation: Delete record by unique Id key Standard API for delete existing record in DB table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/TemplateList/{id}")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Delete Record", Description = "Async standard Delete record API", OperationId = "Delete Record", Tags = new[] { "TemplateListApi" })]
        public async Task<string> DeleteTemplateList(string id) {
            try {
                if (!int.TryParse(id, out int Ids)) return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = 0, ErrorMessage = "Id is not set" });

                TemplateList record = new() { Id = int.Parse(id) };

                var data = new EASYDATACenterContext().TemplateLists.Remove(record);
                int result = await data.Context.SaveChangesAsync();
                if (result > 0) return JsonSerializer.Serialize(new DBResultMessage() { InsertedId = record.Id, Status = DBResult.success.ToString(), RecordCount = result, ErrorMessage = string.Empty });
                else return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = result, ErrorMessage = string.Empty });
            } catch (Exception ex) {
                return JsonSerializer.Serialize(new DBResultMessage() { Status = DBResult.error.ToString(), RecordCount = 0, ErrorMessage = ServerCoreFunctions.GetUserApiErrMessage(ex) });
            }
        }
    }
}
------------------------------------------------------------------------------------------------------------
using EASYDATACenter.DBModel;

namespace EASYDATACenter.Controllers {

    [ApiController]
    [Route("TemplateProcedure")]
    public class TemplateProcedureApi : ControllerBase {
        /// <summary>
        /// API With response from DATABASE STORED PROCEDURE Procedure use IN/OUT parameters In
        /// Database User must have right for Execute procedure
        /// </summary>
        /// <param name="unlockCode"></param>
        /// <param name="partNumber"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("/TemplateProcedure/{UnlockCode}/{PartNumber}")]
        public async Task<string> GetTemplateProcedure(string unlockCode, string partNumber) {
            string data = string.Empty; List<SqlParameter> parameters = new(); bool allowed = false; bool catched = false;
            try {
                if (HttpContext.Connection.RemoteIpAddress != null) {
                    string clientIPAddr = System.Net.Dns.GetHostEntry(HttpContext.Connection.RemoteIpAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
                    if (!string.IsNullOrWhiteSpace(clientIPAddr)) {
                        parameters = new List<SqlParameter> {
                        new SqlParameter { ParameterName = "@unlockCode", Value = unlockCode },
                        new SqlParameter { ParameterName = "@partNumber", Value = partNumber },
                        new SqlParameter { ParameterName = "@ipAddress", Value = clientIPAddr },
                        new SqlParameter { ParameterName = "@allowed" , Value = allowed, Direction = System.Data.ParameterDirection.Output} };

                        data = new EASYDATACenterContext().Database.ExecuteSqlRaw("exec CheckUnlockKey @unlockCode, @partNumber , @ipAddress, @allowed output", parameters.ToArray()).ToString();
                        allowed = bool.Parse(parameters[3].Value.ToString());
                    }
                }
            } catch { catched = true; }
            try {
                if (catched) {
                    parameters = new List<SqlParameter> {
                    new SqlParameter { ParameterName = "@unlockCode", Value = unlockCode },
                    new SqlParameter { ParameterName = "@partNumber", Value = partNumber },
                    new SqlParameter { ParameterName = "@ipAddress", Value = "Unknown" },
                    new SqlParameter { ParameterName = "@allowed" , Value = allowed, Direction = System.Data.ParameterDirection.Output} };

                    data = new EASYDATACenterContext().Database.ExecuteSqlRaw("exec CheckUnlockKey @unlockCode, @partNumber , @ipAddress, @allowed output", parameters.ToArray()).ToString();
                    allowed = bool.Parse(parameters[3].Value.ToString());
                }
            } catch { }
            return JsonSerializer.Serialize(allowed);
        }

        [AllowAnonymous]
        [HttpPost("TemplateProcedure")]
        [Consumes("application/json")]
        public async Task<string> PostTemplateProcedure([FromBody] LicenseActivator record) {
            string data = string.Empty; List<SqlParameter> parameters = new(); bool allowed = false; bool catched = false;
            try {
                if (HttpContext.Connection.RemoteIpAddress != null) {
                    string clientIPAddr = System.Net.Dns.GetHostEntry(HttpContext.Connection.RemoteIpAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
                    if (!string.IsNullOrWhiteSpace(clientIPAddr)) {
                        parameters = new List<SqlParameter> {
                        new SqlParameter { ParameterName = "@unlockCode", Value = record.UnlockCode },
                        new SqlParameter { ParameterName = "@partNumber", Value = record.PartNumber },
                        new SqlParameter { ParameterName = "@ipAddress", Value = clientIPAddr },
                        new SqlParameter { ParameterName = "@allowed" , Value = allowed, Direction = System.Data.ParameterDirection.Output} };

                        data = new EASYDATACenterContext().Database.ExecuteSqlRaw("exec CheckUnlockKey @unlockCode, @partNumber , @ipAddress, @allowed output", parameters.ToArray()).ToString();
                        allowed = bool.Parse(parameters[3].Value.ToString());
                    }
                }
            } catch { catched = true; }
            try {
                if (catched) {
                    parameters = new List<SqlParameter> {
                        new SqlParameter { ParameterName = "@unlockCode", Value = record.UnlockCode },
                        new SqlParameter { ParameterName = "@partNumber", Value = record.PartNumber },
                        new SqlParameter { ParameterName = "@ipAddress", Value = "Unknown" },
                        new SqlParameter { ParameterName = "@allowed" , Value = allowed, Direction = System.Data.ParameterDirection.Output} };

                    data = new EASYDATACenterContext().Database.ExecuteSqlRaw("exec CheckUnlockKey @unlockCode, @partNumber , @ipAddress, @allowed output", parameters.ToArray()).ToString();
                    allowed = bool.Parse(parameters[3].Value.ToString());
                }
            } catch { }
            return JsonSerializer.Serialize(allowed);
        }
    }
}

------------------------------------------------------------------------------------------------------------

using EASYDATACenter.DBModel;

namespace EASYDATACenter.Controllers {
    [Authorize]
    [ApiController]
    [Route("TemplateAnySProcedureData")]
    public class TemplateAnySProcedureDataListApi : ControllerBase {
        /// <summary>
        /// Gets the template any s procedure data list. Simple Call Procedure and the Table Result
        /// is Fill to List od Class Class must Have same Column Names as in SP result
        /// 'CollectionFromSql' and Your Class as Generic 'CollectionFromSql' is Extended for Main
        /// EASYDATACenter Schema If you implement other new schema, you must copy these extensions
        /// </summary>
        /// <returns></returns>
        [HttpGet("/TemplateAnySProcedureData")]
        public async Task<string> GetTemplateAnySProcedureDataList() {
            List<CustomString> data = new();
            data = new EASYDATACenterContext().CollectionFromSql<CustomString>("EXEC GetTables;");

            return JsonSerializer.Serialize(data, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true });
        }
    }
}
------------------------------------------------------------------------------------------------------------
namespace EASYDATACenter.DBModel {
    /// <summary>
    /// Template System Class, This Class has all DBLogic auto Fields and user join for simple
    /// creating system Only Rename for your new table
    /// !!! All Classes Are Generated Automatically For Backend Server And System Builder
    /// </summary>
    [Table("TemplateClassList")]
    [Index("Name", Name = "IX_TemplateClassList", IsUnique = true)]
    public partial class TemplateClassList {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "text")]
        public string? Description { get; set; }

        public int UserId { get; set; }
        public bool Default { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("TemplateClassLists")]
        public virtual UserList User { get; set; } = null!;
    }
}

------------------------------------------------------------------------------------------------------------

using MimeKit;
using EASYDATACenter.DBModel;

namespace EASYDATACenter.Controllers {
    /// <summary>
    /// This is Template for Images API for Returning files Its standard using for WebPages or can
    /// be used for Document System Can be used for All file types
    /// </summary>
    [ApiController]
    [Route("TemplateImageApi/Image")]
    public class TemplateImageApi : ControllerBase {

        /// <summary>
        /// Return Image by Primary Key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/TemplateImageApi/Image/{id}")]
        public async Task<IActionResult> GetSearchImageById(int? id = null) {
            int recId; ImageGalleryList data = null;
            if (!string.IsNullOrWhiteSpace(id.ToString()) && int.TryParse(id.ToString(), out recId)) {
                data = _dbContext.ImageGalleryLists.Where(a => a.Id == recId).FirstOrDefault();
            }

            if (data != null) { return File(data.Attachment, MimeTypes.GetMimeType(data.FileName), data.FileName); } else { return BadRequest(new { message = DBOperations.DBTranslate("BadRequest", "en") }); }
        }

        /// <summary>
        /// Return Image by Foreign Key / or Hidden Logic Previous Attackers
        /// </summary>
        /// <param name="id">      </param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("/TemplateImageApi/Image/{id}/{filename}")]
        public async Task<IActionResult> GetSearchImageByKeys(int? id = null, string fileName = null) {
            int recId; ImageGalleryList data = null;
            if (!string.IsNullOrWhiteSpace(id.ToString()) && !string.IsNullOrWhiteSpace(fileName) && int.TryParse(id.ToString(), out recId)) {
                data = _dbContext.ImageGalleryLists.Where(a => a.Id == recId && a.FileName.ToLower() == fileName.ToLower()).FirstOrDefault();
            }

            if (data != null) { return File(data.Attachment, MimeTypes.GetMimeType(data.FileName), data.FileName); } else { return BadRequest(new { message = DBOperations.DBTranslate("BadRequest", "en") }); }
        }

        [HttpGet("/TemplateImageApi/RoomImage/{id}/{roomId}")]
        public async Task<IActionResult> GetRoomImageByKeys(int? id = null, int? roomId = null) {
            int hId; int rId; ImageGalleryList data = null;
            if (int.TryParse(id.ToString(), out hId) && int.TryParse(roomId.ToString(), out rId)) {
                data = _dbContext.ImageGalleryLists.Where(a => a.Id == hId && a.Id == rId).FirstOrDefault();
            }

            if (data != null) { return File(data.Attachment, MimeTypes.GetMimeType(data.FileName), data.FileName); } else { return BadRequest(new { message = DBOperations.DBTranslate("BadRequest", "en") }); }
        }
    }
}
------------------------------------------------------------------------------------------------------------
