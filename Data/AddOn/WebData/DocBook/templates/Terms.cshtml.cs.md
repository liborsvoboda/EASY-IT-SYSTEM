ď»żusing Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServerCorePages {

    public class TermsModel : PageModel {
        public ServerWebPagesToken serverWebPagesToken;

        private readonly ILogger<TermsModel> _logger;
        public TermsModel(ILogger<TermsModel> logger) {
            _logger = logger;
        }

        public void OnGet() {

            string? requestToken = HttpContext.Request.Cookies.FirstOrDefault(a => a.Key == "ApiToken").Value;
            if (!string.IsNullOrWhiteSpace(requestToken)) {
                serverWebPagesToken = ServerCoreHelpers.CheckTokenValidityFromString(requestToken);
                if (serverWebPagesToken.IsValid) { User.AddIdentities(serverWebPagesToken.UserClaims.Identities); }
            }
            else { serverWebPagesToken = new(); }
        }
    }
}