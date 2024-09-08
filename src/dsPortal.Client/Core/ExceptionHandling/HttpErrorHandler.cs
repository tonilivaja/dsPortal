namespace dsPortal.Client.Core.ExceptionHandling;

public class HttpErrorHandler : DelegatingHandler
{
    private readonly IHttpErrorService _httpErrorService;

    public HttpErrorHandler(IHttpErrorService httpErrorService)
    {
        _httpErrorService = httpErrorService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            await _httpErrorService.EnsureSuccessAsync(response);

            return response;
        }
        catch
        {
            throw;
        }
        finally
        {
            await Task.CompletedTask;
        }
    }
}
