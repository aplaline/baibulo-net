using System;
using System.IO;
using System.Web;

using log4net;

//
// Example usage using cURL:
//
// curl -v -X PUT --data-binary "@image.png" -H "Version: 3" http://localhost:8080/content/test.png
//

namespace Baibulo {
    public class StaticContentUploader: IHttpHandler {
        private static readonly ILog log = LogManager.GetLogger(typeof(StaticContentUploader));

        public bool IsReusable {
            get { return true; }
        }

        private static readonly ResourceManager manager = new ResourceManager(new CompoundVersionExtractor(
            new QueryStringVersionExtractor(),
            new VersionHeaderVersionExtractor()
        ));

        public void ProcessRequest(HttpContext context) {
            var path = manager.GetRequestedPath(context.Request);
            var version = manager.GetRequestedVersion(context.Request);
            log.Info("Processing " + path + " in version " + version);
            if (version == null) {
                SendNoVersionSpecifiedError(context.Response);
            } else {
                try {
                    SaveContentToFile(context.Request.InputStream, path, version);
                    SendResponseOK(context.Response, path, version);
                    log.Info("Processing " + path + " in version " + version + " completed");
                } catch (Exception e) {
                    log.Info("Error while processing " + path + " in version " + version + ": " + e.Message);
                    SendErrorWhileUploadingError(context.Response, e.Message);
                }
            }
        }

        private static void SaveContentToFile(Stream source, string path, string version) {
            Directory.CreateDirectory(path);
            var file = File.Create(path + "/" + version);
            try {
                source.CopyTo(file);
            } finally {
                file.Close();
            }
        }

        private void SendNoVersionSpecifiedError(HttpResponse response) {
            response.StatusCode = 400;
            response.Write("No version specified\n");
        }

        private void SendErrorWhileUploadingError(HttpResponse response, string message) {
            response.StatusCode = 400;
            response.Write("Error while uploading: " + message + "\n");
        }

        private void SendResponseOK(HttpResponse response, string path, string version) {
            response.StatusCode = 201;
            response.Write(String.Format("File {0} in version {1} stored successfully\n", path, version));
        }
    }
}
