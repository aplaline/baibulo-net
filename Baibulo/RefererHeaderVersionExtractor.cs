using System;
using System.Web;

using log4net;

namespace Baibulo {
    public class RefererHeaderVersionExtractor: IVersionExtractor {
        private static readonly ILog log = LogManager.GetLogger(typeof(RefererHeaderVersionExtractor));

        public string extractVersionFromRequest(HttpRequest request) {
            var header = request.Headers["Referer"];
            if (header != null) {
                var referer = new Uri(header);
                var result = HttpUtility.ParseQueryString(referer.Query)["version"];
                if (result != null) {
                    log.Info("Found version " + result);
                    return result;
                } else {
                    return null;
                }
            } else {
                return null;
            }
        }
    }
}
