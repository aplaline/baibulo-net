using System.Web;

using log4net;

namespace Baibulo {
    public class QueryStringVersionExtractor: IVersionExtractor {
        private static readonly ILog log = LogManager.GetLogger(typeof(QueryStringVersionExtractor));

        public string extractVersionFromRequest(HttpRequest request) {
            var result = request.QueryString["version"];
            if (result != null) {
                log.Info("Found version " + result);
                return result;
            } else {
                return null;
            }
        }
    }
}
