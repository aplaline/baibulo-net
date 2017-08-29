using System.Web;

namespace Baibulo {
    public class QueryStringVersionExtractor: IVersionExtractor {
        public string extractVersionFromRequest(HttpRequest request) {
            return request.QueryString["version"];
        }
    }
}
