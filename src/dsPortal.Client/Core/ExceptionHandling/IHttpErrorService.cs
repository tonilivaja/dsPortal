namespace dsPortal.Client.Core.ExceptionHandling;

public interface IHttpErrorService
{
    event EventHandler<ErrorMessageEventArgs> HttpErrorHandled;

    Task EnsureSuccessAsync(HttpResponseMessage response);
}