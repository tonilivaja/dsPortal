using Microsoft.AspNetCore.Components;
using System.Net;

namespace dsPortal.Client.Core.ExceptionHandling;

public class HttpErrorService : IHttpErrorService
{
    private readonly NavigationManager _navigation;

    public event EventHandler<ErrorMessageEventArgs> HttpErrorHandled;

    public HttpErrorService(NavigationManager navigation)
    {
        _navigation = navigation;
    }

    public async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        string errorMessage = string.Empty;
        if (!response.IsSuccessStatusCode)
        {
            var statusCode = response.StatusCode;

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.TooManyRequests:
                    errorMessage = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        HttpErrorHandled?.Invoke(this, new ErrorMessageEventArgs(errorMessage, LogLevel.Warning));
                    }
                    else
                    {
                        errorMessage = "Something went wrong!";
                        HttpErrorHandled?.Invoke(this, new ErrorMessageEventArgs(errorMessage, LogLevel.Warning));
                    }

                    break;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.MethodNotAllowed:

                    _navigation.NavigateTo("/unauthorized");
                    errorMessage = "User is not authorized";
                    break;
                case HttpStatusCode.Conflict:
                    errorMessage = await response.Content.ReadAsStringAsync();
                    if (errorMessage != null)
                    {
                        HttpErrorHandled?.Invoke(this, new ErrorMessageEventArgs(errorMessage, LogLevel.Warning));
                    }
                    else
                    {
                        errorMessage = "Item can not be updated/created because it is not unique.";
                        HttpErrorHandled?.Invoke(this, new ErrorMessageEventArgs(errorMessage, LogLevel.Warning));
                    }
                    break;
                default:

                    _navigation.NavigateTo("/error");
                    errorMessage = "Something went wrong!";
                    break;
            }

            throw new ApplicationException(errorMessage);
        }
    }
}
