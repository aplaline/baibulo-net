using System.Web;

namespace Baibulo {
    public class VersionHeaderVersionExtractor: IVersionExtractor {
        public string extractVersionFromRequest(HttpRequest request) {
            return request.Headers["Version"];
        }
    }
}
