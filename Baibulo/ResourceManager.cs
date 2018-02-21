using System;
using System.IO;
using System.Web;
using System.Configuration;

namespace Baibulo {
    public class ResourceManager {
        private readonly IVersionExtractor versionExtractor;
        private readonly String root;

        public ResourceManager(IVersionExtractor versionExtractor) {
            this.versionExtractor = versionExtractor;
            this.root = ConfigurationManager.AppSettings["baibulo-root"] ?? "../..";
        }

        public string GetRequestedPath(HttpRequest request) {
            return Path.GetFullPath(root + request.Path);
        }

        public string GetRequestedVersion(HttpRequest request) {
            return versionExtractor.extractVersionFromRequest(request);
        }

        public bool ResourceExistsInVersion(string path, string version) {
            return File.Exists(path + "/" + version);
        }

        public bool ResourceLocationExists(string path) {
            return File.Exists(path);
        }
    }
}
