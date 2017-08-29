using System.Web;

namespace Baibulo {
    public interface IVersionExtractor {
        string extractVersionFromRequest(HttpRequest request);
    }
}
