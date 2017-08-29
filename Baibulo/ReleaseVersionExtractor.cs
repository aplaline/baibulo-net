using System.Web;

namespace Baibulo {
    public class ReleaseVersionExtractor: IVersionExtractor {
        public string extractVersionFromRequest(HttpRequest request) {
            return "release";
        }
    }
}
