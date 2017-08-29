using System;
using System.Web;

namespace Baibulo {
    public class RefererHeaderVersionExtractor: IVersionExtractor {
        public string extractVersionFromRequest(HttpRequest request) {
            var header = request.Headers["Referer"];
            if (header != null) {
                var referer = new Uri(header);
                return HttpUtility.ParseQueryString(referer.Query)["version"];
            } else {
                return null;
            }
        }
    }
}
