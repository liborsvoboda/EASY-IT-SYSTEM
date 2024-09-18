ď»żusing EASYDATACenter.DBModel;
using EASYDATACenter.ServerCoreDefinition;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServerCorePages {

    public class WebEditorsModel : PageModel {
        public static ServerWebPagesToken serverWebPagesToken;

        public void OnGet() {

            string? requestToken = HttpContext.Request.Cookies.FirstOrDefault(a => a.Key == "ApiToken").Value;
            if (!string.IsNullOrWhiteSpace(requestToken)) {
                serverWebPagesToken = ServerCoreHelpers.CheckTokenValidityFromString(requestToken);
                if (serverWebPagesToken.IsValid) { User.AddIdentities(serverWebPagesToken.UserClaims.Identities); }
            }
            else { serverWebPagesToken = new ServerWebPagesToken(); }
        }
    }
}