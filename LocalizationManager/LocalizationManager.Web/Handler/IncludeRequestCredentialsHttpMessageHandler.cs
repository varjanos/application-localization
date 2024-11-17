﻿using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace LocalizationManager.Web.Handler;

public class IncludeRequestCredentialsHttpMessageHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        return base.SendAsync(request, cancellationToken);
    }
}