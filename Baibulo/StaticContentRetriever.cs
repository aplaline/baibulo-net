using System.Web;

namespace Baibulo {
    public partial class StaticContentRetriever: IHttpHandler {
        public bool IsReusable => true;

        private static readonly ResourceManager manager = new ResourceManager(new CompoundVersionExtractor(
            new QueryStringVersionExtractor(),
            new VersionHeaderVersionExtractor(),
            new RefererHeaderVersionExtractor(),
            new ReleaseVersionExtractor()
        ));

        public void ProcessRequest(HttpContext context) {
            var path = manager.GetRequestedPath(context.Request);
            var version = manager.GetRequestedVersion(context.Request);
            if (manager.ResourceExistsInVersion(path, version)) {
                SendResourceInVersion(context.Response, path, version);
            } else {
                SendResourceNotFound(context.Response);
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
