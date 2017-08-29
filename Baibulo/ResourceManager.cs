using System;
using System.IO;
using System.Web;

namespace Baibulo {
    public class ResourceManager {
        private readonly IVersionExtractor versionExtractor;

        public ResourceManager(IVersionExtractor versionExtractor) {
            this.versionExtractor = versionExtractor;    
        }

        public string GetRequestedPath(HttpRequest request) {
            return Path.GetFullPath("../.." + request.Path);
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
