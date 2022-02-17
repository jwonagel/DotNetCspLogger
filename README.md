# DotNetCspLogger
Simple API to Log Content Security Policy Reports


## Config

Just set the LogPath in appsettings and start.


## CSP Header

Set the two http Header like:

```http
Content-Security-Policy-Report-Only: policy
Content-Security-Policy: default-src 'self'  [further rules]; report-uri {uriOfTheAPi}/ContentSecurityPolicy

```

How to configure CSP look at [Content Security Policy (CSP)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP)