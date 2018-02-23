using System.Web;

using log4net;

namespace Baibulo {
    public partial class StaticContentRetriever: IHttpHandler {
        private static readonly ILog log = LogManager.GetLogger(typeof(StaticContentRetriever));

        public bool IsReusable {
            get { return true; }
        }

        private static readonly ResourceManager manager = new ResourceManager(new CompoundVersionExtractor(
            new QueryStringVersionExtractor(),
            new VersionHeaderVersionExtractor(),
            new RefererHeaderVersionExtractor(),
            new CookieVersionExtractor(),
            new ReleaseVersionExtractor()
        ));

        public void ProcessRequest(HttpContext context) {
            var path = manager.GetRequestedPath(context.Request);
            var version = manager.GetRequestedVersion(context.Request);
            log.Info("Processing " + path + " in version " + version);
            if (manager.ResourceExistsInVersion(path, version)) {
                CookieVersionExtractor.SetVersionCookie(context.Response, version);
                SendResourceInVersion(context.Response, path, version);
                log.Info("Processing completed");
            } else {
                SendResourceNotFound(context.Response);
                log.Info("Requested resource not found");
            }
        }

        private void SendResourceInVersion(HttpResponse response, string path, string version) {
            response.ContentType = MimeMapping.GetMimeMapping(path);
            response.WriteFile(path + "/" + version);
        }

        private void SendResourceNotFound(HttpResponse response) {
            response.ContentType = "text/plain";
            response.StatusCode = 404;
            response.Write("Not found\n");
        }
    }
}
