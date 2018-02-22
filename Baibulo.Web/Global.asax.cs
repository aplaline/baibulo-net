using System.Web;

using log4net.Config;

namespace Baibulo.Web {
    public class Global: HttpApplication {
        protected void Application_Start() {
            BasicConfigurator.Configure();
        }
    }
}
