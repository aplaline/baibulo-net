using System.Web;

using log4net;

namespace Baibulo {
    public class VersionHeaderVersionExtractor: IVersionExtractor {
        private static readonly ILog log = LogManager.GetLogger(typeof(VersionHeaderVersionExtractor));

        public string extractVersionFromRequest(HttpRequest request) {
            var result = request.Headers["Version"];
            if (result != null) {
                log.Info("Found version " + result);
                return result;
            } else {
                return null;
            }
        }
    }
}
