using System;
using System.Linq;
using System.Web;

namespace Baibulo {
    public class CompoundVersionExtractor: IVersionExtractor {
        private readonly IVersionExtractor[] extractors;

        public CompoundVersionExtractor(params IVersionExtractor[] extractors) {
            this.extractors = extractors;
        }

        public string extractVersionFromRequest(HttpRequest request) {
            return extractors
                .Select(extractor => extractor.extractVersionFromRequest(request))
                .Where(version => version != null)
                .FirstOrDefault();
        }
    }
}
