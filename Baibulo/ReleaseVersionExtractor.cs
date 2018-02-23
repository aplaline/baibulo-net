using System.Web;

using log4net;

namespace Baibulo {
    public class ReleaseVersionExtractor: IVersionExtractor {
        private static readonly ILog log = LogManager.GetLogger(typeof(ReleaseVersionExtractor));

        public string extractVersionFromRequest(HttpRequest request) {
            log.Info("Assuming release version");
            return "release";
        }
    }
}
