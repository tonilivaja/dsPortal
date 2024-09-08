namespace dsPortal.Client.Core.ExceptionHandling;

public class ErrorMessageEventArgs : EventArgs
{
    public string Message { get; set; }

    public LogLevel Severity { get; set; }

    public ErrorMessageEventArgs(string message, LogLevel severity)
    {
        Message = message;
        Severity = severity;
    }
}