## Baibulo - versioned static content server and manager

Baibulo (*version* in Chewa) is a versioned static content server and manager package for Node express applications.
It is a version of implementation of approach [presented](https://www.youtube.com/watch?v=QZVYP3cPcWQ) on RailsConf 2014 by Luke Melia.

### Installation

The package is available in nuget.org repository therefore installation of the latest version is very easy:

Package manager
```
PM> Install-Package baibulo-net
```

.NET CLI
```
> dotnet add package baibulo-net
```

Packet CLI
```
> paket add baibulo-net
```

NuGet
```
> nuget install baibulo-net
```


### Usage

In your main ```web.config``` configure file limits:

```
<configuration>
  <system.web>
    <!-- To increase ASP.NET file upload limit set httpRuntime maxRequestLength (in KB) -->
    <httpRuntime maxRequestLength="4096" />
  </system.web>

  <!-- To increase ISS file upload limit set maxAllowedContentLength (in bytes) -->
  <system.webServer>
    <security>
      <requestFiltering>
        <requestLimits maxAllowedContentLength="1073741824" />
      </requestFiltering>
    </security>
  </system.webServer>
</configuration>
```

Then in a subfolder with your assets add the following configuration to ```web.config```

```
<configuration>
  <appSettings>
    <add key="baibulo-root" value="/tmp/"/>
  </appSettings>

  <system.web>
    <httpHandlers>
      <add verb="GET" path="*" type="Baibulo.StaticContentRetriever" />
      <add verb="PUT" path="*" type="Baibulo.StaticContentUploader" />
    </httpHandlers>
  </system.web>
</configuration>
```

That's it! Now GET will return versioned content and PUT will allow to upload it.

### Uploading content

For uploading content use either a custom HTTP client like [cURL](https://curl.haxx.se/docs/manpage.html) or [NPM baibulo-deploy package](https://www.npmjs.com/package/baibulo-deploy).

### Additional informations

Baibulo uses file system to store versioned static content. By default it uses a 2-level-up path but it can be configured in appSettings under ```baibulo-root``` key.
