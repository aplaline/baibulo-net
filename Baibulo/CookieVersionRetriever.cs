using System;
using System.Web;

using log4net;

namespace Baibulo {
    public class CookieVersionExtractor : IVersionExtractor {
        private static readonly ILog log = LogManager.GetLogger(typeof(CookieVersionExtractor));

        public string extractVersionFromRequest(HttpRequest request) {
            var cookie = request.Cookies["Version"];
            if (cookie != null && cookie.Value != "") {
                log.Info("Found version " + cookie.Value);
                return cookie.Value;
            } else {
                return null;
            }
        }
    }
}
