using System;
using System.Web;

using log4net;

namespace Baibulo {
    public class CookieVersionExtractor : IVersionExtractor {
        private const string CookieName = "__version";
        private static readonly ILog log = LogManager.GetLogger(typeof(CookieVersionExtractor));

        public string extractVersionFromRequest(HttpRequest request) {
            var cookie = request.Cookies[CookieName];
            if (cookie != null && cookie.Value != "") {
                log.Info("Found version " + cookie.Value);
                return cookie.Value;
            } else {
                return null;
            }
        }

        public static void SetVersionCookie(HttpResponse response, string version) {
            response.SetCookie(new HttpCookie(CookieName) { Value = version });
        }
    }
}
