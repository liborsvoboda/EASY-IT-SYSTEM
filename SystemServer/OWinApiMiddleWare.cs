using Microsoft.Owin;
using System.Threading.Tasks;

namespace EasyITSystemCenter.WebServer {

    public class SampleOwinMiddleware : OwinMiddleware {
        public SampleOwinMiddleware(OwinMiddleware next)
            : base(next) {
        }
        public async override Task Invoke(IOwinContext context) {
            context.Response.Headers.Append("SystempApi", "OWIN");

            await Next.Invoke(context);
        }
    }
}