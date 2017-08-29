using System;
using System.IO;
using System.Web;

//
// Example usage using cURL:
//
// curl -v -X PUT --data-binary "@components.png" -H "Version: 3" http://localhost:8080/content/test.png
//

namespace Baibulo {
    public class StaticContentUploader: IHttpHandler {
        public bool IsReusable => true;

        private static readonly ResourceManager manager = new ResourceManager(new CompoundVersionExtractor(
            new QueryStringVersionExtractor(),
            new VersionHeaderVersionExtractor()
        ));

        public void ProcessRequest(HttpContext context) {
            var path = manager.GetRequestedPath(context.Request);
            var version = manager.GetRequestedVersion(context.Request);
            Directory.CreateDirectory(path);
            SaveContentToFile(context.Request.InputStream, path, version);
            SendResponseOK(context.Response, path, version);
        }

        private static void SaveContentToFile(Stream source, string path, string version) {
            var file = File.Create(path + "/" + version, 4096, FileOptions.WriteThrough);
            source.CopyTo(file);
            file.Close();
        }

        private void SendResponseOK(HttpResponse response, string path, string version) {
            response.Write(String.Format("File {0} in version {1} stored successfully\n", path, version));
        }
    }
}
